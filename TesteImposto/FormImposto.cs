using Imposto.Core.Service;
using Imposto.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();

            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            dataGridViewPedidos.Columns["Desconto"].ReadOnly = true;
            dataGridViewPedidos.Columns["Desconto"].DefaultCellStyle.Format = "0\\%";

            EstadoService estadoService = new EstadoService();
            cboEstadoOrigem.DataSource = estadoService.CarregarEstados();
            cboEstadoOrigem.SelectedIndex = -1;
            cboEstadoDestino.DataSource = estadoService.CarregarEstados();
            cboEstadoDestino.SelectedIndex = -1;

            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = (dataGridViewPedidos.Width - dataGridViewPedidos.RowHeadersWidth) / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Desconto", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (textBoxNomeCliente.Text == "" || cboEstadoOrigem.SelectedItem == null || cboEstadoDestino.SelectedItem == null)
            {
                MessageBox.Show(this, "Necessário preencher os dados da nota fiscal", buttonGerarNotaFiscal.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;
            if (table.Rows.Count == 0)
            {
                MessageBox.Show(this, "Nenhum item adicionado", buttonGerarNotaFiscal.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NotaFiscalService service = new NotaFiscalService();
            pedido.EstadoOrigem = (Estado)cboEstadoOrigem.SelectedItem;
            pedido.EstadoDestino = (Estado)cboEstadoDestino.SelectedItem;
            pedido.NomeCliente = textBoxNomeCliente.Text;

            foreach (DataRow row in table.Rows)
            {
                if (row.IsNull("Nome do produto") || row.IsNull("Codigo do produto"))
                {
                    MessageBox.Show(this, "Preencher o nome e o código de todos os itens", buttonGerarNotaFiscal.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (row.IsNull("Brinde")) { row["Brinde"] = false; }
                if (row.IsNull("Valor"))
                {
                    MessageBox.Show(this, "Preencher o valor de todos os itens", buttonGerarNotaFiscal.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (row.IsNull("Desconto")) { row["Desconto"] = 0; }

                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = Convert.ToBoolean(row["Brinde"]),
                        CodigoProduto =  row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString()),
                        Desconto = Convert.ToDouble(row["Desconto"])
                    });
            }

            try
            {
                service.GerarNotaFiscal(pedido);
                MessageBox.Show(this, "Operação efetuada com sucesso", buttonGerarNotaFiscal.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpaCampos();
            }
            catch (ServiceException ex)
            {
                MessageBox.Show(this, ex.Message, buttonGerarNotaFiscal.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormImposto_Resize(object sender, EventArgs e)
        {
            ResizeColumns();
        }

        private void cboEstadoDestino_SelectedValueChanged(object sender, EventArgs e)
        {
            CalcularDescontos();
        }

        private void dataGridViewPedidos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalcularDescontos();
        }

        private void CalcularDescontos()
        {
            if (cboEstadoDestino.SelectedItem == null) { return; }
            foreach (DataGridViewRow row in dataGridViewPedidos.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["Desconto"].Value = ((Estado)cboEstadoDestino.SelectedItem).Desconto * 100;
                }
            }
        }

        private void dataGridViewPedidos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            decimal valor;
            if (e.FormattedValue.ToString() != "" &&
                dataGridViewPedidos.Columns[e.ColumnIndex].ValueType == typeof(decimal) &&
                !decimal.TryParse(dataGridViewPedidos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out valor))
            {
                MessageBox.Show(this, "Valor inválido", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void LimpaCampos()
        {
            textBoxNomeCliente.Text = "";
            cboEstadoOrigem.SelectedIndex = -1;
            cboEstadoDestino.SelectedIndex = -1;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            textBoxNomeCliente.Focus();
        }
    }
}
