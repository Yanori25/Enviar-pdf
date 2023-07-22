using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnviandoPDF.Capacontroller;
using EnviandoPDF.Capamodelo;
using System.Drawing.Printing;




namespace EnviandoPDF.Capaview
{
    public partial class Usuario : Form

    {
        Empleadocontroller emple = new Empleadocontroller();

        public Usuario()
        {
            InitializeComponent();
        }
        private void refrescar()
        {
            dgvempleados.DataSource = emple.getempleados();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.refrescar();
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            this.refrescar();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvempleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtempleado_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnom_TextChanged(object sender, EventArgs e)
        {
            string filtername = "NombreUsuario";
            ((DataTable)dgvempleados.DataSource).DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filtername,
                txtnom.Text);
        }

        private void textRol_TextChanged(object sender, EventArgs e)
        {
            string filtername = "RolUsuario";
            ((DataTable)dgvempleados.DataSource).DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filtername,
                txtRol.Text);
        }

        private void txtCod_TextChanged(object sender, EventArgs e)
        {
            string filtername = "codigoempleado";
            ((DataTable)dgvempleados.DataSource).DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filtername,
                txtCod.Text);
        }

        private void textApe_TextChanged(object sender, EventArgs e)
        {
            string filtername = "ApelldoUsuario";
            ((DataTable)dgvempleados.DataSource).DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filtername,
                textApe.Text);
        }

        private int i = -1;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // La fuente que vamos a usar para imprimir.
            Font printFont = new Font("Arial", 10);
            float topMargin = e.MarginBounds.Top;
            float yPos = 0;
            float linesPerPage = 0;
            int count = 0;
            string texto = "";
            DataGridViewRow row;

            // Calculamos el número de líneas que caben en cada página.
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);


            // Recorremos las filas del DataGridView hasta que llegemos
            // a las líneas que nos caben en cada página o al final del grid.
            while (count < linesPerPage && i < this.dgvempleados.Rows.Count)
            {
                row = dgvempleados.Rows[i];
                texto = "";

                foreach (DataGridViewCell celda in row.Cells)
                {
                    texto += "\t" + celda.Value.ToString();
                }

                // Calculamos la posición en la que se escribe la línea
                yPos = topMargin + (count * printFont.GetHeight(e.Graphics));

                // Escribimos la línea con el objeto Graphics
                e.Graphics.DrawString(texto, printFont, Brushes.Black, 10, yPos);

                count++;
                i++;
            }

            // Una vez fuera del bucle comprobamos si nos quedan más filas 
            // por imprimir, si quedan saldrán en la siguente página
            if (i < this.dgvempleados.Rows.Count)
                e.HasMorePages = true;
            else
            {
                // si llegamos al final, se establece HasMorePages a 
                // false para que se acabe la impresión
                e.HasMorePages = false;

                // Es necesario poner el contador a 0 porque, por ejemplo si se hace 
                // una impresión desde PrintPreviewDialog, se vuelve disparar este 
                // evento como si fuese la primera vez, y si i está con el valor de la
                // última fila del grid no se imprime nada
                i = 0;
            }

        }


        private void btnimprimir_Click(object sender, EventArgs e)
        {
       

            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = doc };
            ((Form)ppd).WindowState = FormWindowState.Maximized;

            doc.PrintPage += delegate (object ev, PrintPageEventArgs ep)
            {
                const int DGV_ALTO = 35;
                float valorhead = 0;
                int left = ep.MarginBounds.Left, top = ep.MarginBounds.Top;
               // float yposition = ep.MarginBounds.Top;


                foreach (DataGridViewColumn col in dgvempleados.Columns)
                {
                    if (col.Index >= 1 && col.Index < 6)
                    {
                       
                            ep.Graphics.DrawString(col.HeaderText, new Font("Arial", 12, FontStyle.Bold), Brushes.DeepSkyBlue, left, top);                 
                        left += col.Width;

                    }

                    
                    //if (col.Index < dgvempleados.ColumnCount - 1)
                    //{
                    //    ep.Graphics.DrawLine(Pens.Gray, left - 5, top, left - 5, top + 43 + (dgvempleados.RowCount - 1) * DGV_ALTO);
                    //}
                    
                }
                left = ep.MarginBounds.Left;
                ep.Graphics.FillRectangle(Brushes.Black, left, top + 40, ep.MarginBounds.Right - left, 3 );
                top += 43;

                foreach(DataGridViewRow row in dgvempleados.Rows)
                {

                    if (row.Index == dgvempleados.RowCount - 1) break;
                    left = ep.MarginBounds.Left;      
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if(cell.ColumnIndex > 0 && cell.ColumnIndex < 6 )
                        {
                            ep.Graphics.DrawString(Convert.ToString(cell.Value), new Font("Arial", 10), Brushes.Black, left, top + 4);
                            left += cell.OwningColumn.Width;
                        }
                       
                        

                    }
                    top += +DGV_ALTO;
                    ep.Graphics.DrawLine(Pens.Gray, ep.MarginBounds.Left, top, ep.MarginBounds.Right, top);

                }
            };

           
            ppd.ShowDialog();
        }
    }
}
    




