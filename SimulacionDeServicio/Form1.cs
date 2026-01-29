using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulacionDeServicio
{
    public partial class Form1 : Form
    {
        private int[] numeros_Llegada;
        private int[] numeros_Servicio;
        private double[] numeros_Servicio_decimal;
        private double[] numeros_Servicio_personalizado;
        private double resultado;
        private double tiempoCola1;
        private double tiempoCola2;
        private double tiempoCola3;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox1.Text.Trim();
            string[] tiempos_de_llegada  = texto.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            numeros_Llegada = new int[tiempos_de_llegada.Length + 1];

            for (int i = 0; i < tiempos_de_llegada.Length; i++)
            {
                int.TryParse(tiempos_de_llegada[i], out numeros_Llegada[i + 1]);
            }
            numeros_Llegada[0] = 0;
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox2.Text.Trim();
            string[] tiempos_de_Servicio = texto.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            numeros_Servicio = new int[tiempos_de_Servicio.Length];
            numeros_Servicio_decimal = new double[tiempos_de_Servicio.Length];
            numeros_Servicio_personalizado = new double[tiempos_de_Servicio.Length];

            for (int i = 0; i < tiempos_de_Servicio.Length; i++)
            {
                int.TryParse(tiempos_de_Servicio[i], out numeros_Servicio[i]);
                if (double.TryParse(tiempos_de_Servicio[i], out double value))
                {
                    numeros_Servicio_decimal[i] = value * 0.8;
                    numeros_Servicio_personalizado[i] = value * resultado;
                }
            }
        }
        public int[] ObtenerNumeros()
        {
            return numeros_Llegada;
        }
        public int[] Tiempo_Servicio()
        {
            return numeros_Servicio;
        }
        public void CargarNumerosEnGrid()
        {
            if (numeros_Llegada == null) return;
            int clientes = Math.Min(numeros_Llegada.Length, numeros_Servicio.Length);

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Cliente", typeof(float));
            tabla.Columns.Add("Tiempo entre llegadas", typeof(float));
            tabla.Columns.Add("Tiempo de llegada", typeof(float));
            tabla.Columns.Add("Tiempo de Servicio", typeof(float));
            tabla.Columns.Add("Inicio de Servicio", typeof(float));
            tabla.Columns.Add("Fin de Servicio", typeof(float));
            tabla.Columns.Add("Tiempo en Cola", typeof(float));
            tabla.Columns.Add("Tiempo en Sistema", typeof(float));
            tabla.Columns.Add("Tiempo de Ocio", typeof(float));

            DataTable tabla_resumen = new DataTable();
            tabla_resumen.Columns.Add("Total Tiempo de Servicio", typeof(float));
            tabla_resumen.Columns.Add("Total Tiempo en Cola", typeof(float));
            tabla_resumen.Columns.Add("Total Tiempo en Sistema", typeof(float));
            tabla_resumen.Columns.Add("Total tiempo de Ocio", typeof(float));

            int tiempo_de_llegada = 0;
            int finServicioAnterior = numeros_Servicio[0];
            int tiempo_en_cola = 0;
            int tiempo_en_sistema = 0;
            int tiempo_de_ocio = 0;

            int totalServicio = 0;
            int totalCola = 0;
            int totalSistema = 0;
            int totalOcio = 0;

            totalServicio += numeros_Servicio[0];
            totalCola += 0;
            totalSistema += numeros_Servicio[0];
            totalOcio += 0;

            tabla.Rows.Add(1, tiempo_de_llegada, numeros_Llegada[0], numeros_Servicio[0], 0, numeros_Servicio[0], 0, numeros_Servicio[0], 0);
            
            for (int i = 1; i < clientes; i++)
            {
                int llegada = numeros_Llegada[i];
                int servicio = numeros_Servicio[i];

                tiempo_de_llegada += llegada;

                int inicio_de_servicio = Math.Max(tiempo_de_llegada, finServicioAnterior);
                int fin_de_servicio = inicio_de_servicio + servicio;
                tiempo_en_cola = Math.Max(0, inicio_de_servicio - tiempo_de_llegada);
                tiempo_en_sistema = servicio + tiempo_en_cola;
                tiempo_de_ocio = Math.Max(0, tiempo_de_llegada - finServicioAnterior);
                finServicioAnterior = fin_de_servicio;

                totalServicio += servicio;
                totalCola += tiempo_en_cola;
                totalSistema += tiempo_en_sistema;
                totalOcio += tiempo_de_ocio;

                tabla.Rows.Add(i + 1, llegada, tiempo_de_llegada, servicio, inicio_de_servicio, fin_de_servicio, tiempo_en_cola, tiempo_en_sistema, tiempo_de_ocio);
            }

            tiempoCola1 = totalCola;

            tabla_resumen.Rows.Add(totalServicio, totalCola, totalSistema, totalOcio);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = tabla;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.DataSource = tabla_resumen;
        }

        public void CargarNumerosEnGrid2()
        {
            if (numeros_Llegada == null) return;
            int clientes = Math.Min(numeros_Llegada.Length, numeros_Servicio_decimal.Length);

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Cliente", typeof(float));
            tabla.Columns.Add("Tiempo entre llegadas", typeof(float));
            tabla.Columns.Add("Tiempo de llegada", typeof(float));
            tabla.Columns.Add("Tiempo de Servicio", typeof(float));
            tabla.Columns.Add("Inicio de Servicio", typeof(float));
            tabla.Columns.Add("Fin de Servicio", typeof(float));
            tabla.Columns.Add("Tiempo en Cola", typeof(float));
            tabla.Columns.Add("Tiempo en Sistema", typeof(float));
            tabla.Columns.Add("Tiempo de Ocio", typeof(float));

            DataTable tabla_resumen = new DataTable();
            tabla_resumen.Columns.Add("Total Tiempo de Servicio", typeof(float));
            tabla_resumen.Columns.Add("Total Tiempo en Cola", typeof(float));
            tabla_resumen.Columns.Add("Total Tiempo en Sistema", typeof(float));
            tabla_resumen.Columns.Add("Total tiempo de Ocio", typeof(float));

            double tiempo_de_llegada = 0;
            double finServicioAnterior = numeros_Servicio_decimal[0];
            double tiempo_en_cola = 0;
            double tiempo_en_sistema = 0;
            double tiempo_de_ocio = 0;

            double totalServicio = 0;
            double totalCola = 0;
            double totalSistema = 0;
            double totalOcio = 0;

            totalServicio += numeros_Servicio_decimal[0];
            totalCola += 0;
            totalSistema += numeros_Servicio_decimal[0];
            totalOcio += 0;

            tabla.Rows.Add(1, tiempo_de_llegada, numeros_Llegada[0], numeros_Servicio_decimal[0], 0, numeros_Servicio_decimal[0], 0, numeros_Servicio_decimal[0], 0);

            for (int i = 1; i < clientes; i++)
            {
                int llegada = numeros_Llegada[i];
                double servicio = numeros_Servicio_decimal[i];

                tiempo_de_llegada += llegada;

                double inicio_de_servicio = Math.Max(tiempo_de_llegada, finServicioAnterior);
                double fin_de_servicio = inicio_de_servicio + servicio;
                tiempo_en_cola = Math.Max(0, inicio_de_servicio - tiempo_de_llegada);
                tiempo_en_sistema = servicio + tiempo_en_cola;
                tiempo_de_ocio = Math.Max(0, tiempo_de_llegada - finServicioAnterior);
                finServicioAnterior = fin_de_servicio;

                totalServicio += servicio;
                totalCola += tiempo_en_cola;
                totalSistema += tiempo_en_sistema;
                totalOcio += tiempo_de_ocio;

                tabla.Rows.Add(i + 1, llegada, tiempo_de_llegada, servicio, inicio_de_servicio, fin_de_servicio, tiempo_en_cola, tiempo_en_sistema, tiempo_de_ocio);
            }

            tabla_resumen.Rows.Add(totalServicio, totalCola, totalSistema, totalOcio);

            tiempoCola2 = totalCola;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.DataSource = tabla;
            dataGridView5.AllowUserToAddRows = false;
            dataGridView5.DataSource = tabla_resumen;
        }

        public void CargarNumerosEnGrid3()
        {
            if (numeros_Llegada == null) return;
            int clientes = Math.Min(numeros_Llegada.Length, numeros_Servicio_decimal.Length);

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Cliente", typeof(float));
            tabla.Columns.Add("Tiempo entre llegadas", typeof(float));
            tabla.Columns.Add("Tiempo de llegada", typeof(float));
            tabla.Columns.Add("Tiempo de Servicio", typeof(float));
            tabla.Columns.Add("Inicio de Servicio", typeof(float));
            tabla.Columns.Add("Fin de Servicio", typeof(float));
            tabla.Columns.Add("Tiempo en Cola", typeof(float));
            tabla.Columns.Add("Tiempo en Sistema", typeof(float));
            tabla.Columns.Add("Tiempo de Ocio", typeof(float));

            DataTable tabla_resumen = new DataTable();
            tabla_resumen.Columns.Add("Total Tiempo de Servicio", typeof(float));
            tabla_resumen.Columns.Add("Total Tiempo en Cola", typeof(float));
            tabla_resumen.Columns.Add("Total Tiempo en Sistema", typeof(float));
            tabla_resumen.Columns.Add("Total tiempo de Ocio", typeof(float));

            double tiempo_de_llegada = 0;
            double finServicioAnterior = numeros_Servicio_personalizado[0];
            double tiempo_en_cola = 0;
            double tiempo_en_sistema = 0;
            double tiempo_de_ocio = 0;

            double totalServicio = 0;
            double totalCola = 0;
            double totalSistema = 0;
            double totalOcio = 0;

            totalServicio += numeros_Servicio_personalizado[0];
            totalCola += 0;
            totalSistema += numeros_Servicio_personalizado[0];
            totalOcio += 0;

            tabla.Rows.Add(1, tiempo_de_llegada, numeros_Llegada[0], numeros_Servicio_personalizado[0], 0, numeros_Servicio_personalizado[0], 0, numeros_Servicio_personalizado[0], 0);

            for (int i = 1; i < clientes; i++)
            {
                int llegada = numeros_Llegada[i];
                double servicio = numeros_Servicio_personalizado[i];

                tiempo_de_llegada += llegada;

                double inicio_de_servicio = Math.Max(tiempo_de_llegada, finServicioAnterior);
                double fin_de_servicio = inicio_de_servicio + servicio;
                tiempo_en_cola = Math.Max(0, inicio_de_servicio - tiempo_de_llegada);
                tiempo_en_sistema = servicio + tiempo_en_cola;
                tiempo_de_ocio = Math.Max(0, tiempo_de_llegada - finServicioAnterior);
                finServicioAnterior = fin_de_servicio;

                totalServicio += servicio;
                totalCola += tiempo_en_cola;
                totalSistema += tiempo_en_sistema;
                totalOcio += tiempo_de_ocio;

                tabla.Rows.Add(i + 1, llegada, tiempo_de_llegada, servicio, inicio_de_servicio, fin_de_servicio, tiempo_en_cola, tiempo_en_sistema, tiempo_de_ocio);
            }

            tabla_resumen.Rows.Add(totalServicio, totalCola, totalSistema, totalOcio);

            tiempoCola3 = totalCola;

            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.DataSource = tabla;
            dataGridView6.AllowUserToAddRows = false;
            dataGridView6.DataSource = tabla_resumen;
        }

        private void btnMostrar_Click_Click(object sender, EventArgs e)
        {
            if (numeros_Llegada == null || numeros_Servicio == null)
            {
                MessageBox.Show("Debes ingresar tiempos de llegada y de servicio.");
                return;
            }
            if (numeros_Llegada.Length != numeros_Servicio.Length)
            {
                int diferencia = Math.Abs(numeros_Llegada.Length - numeros_Servicio.Length);

                MessageBox.Show(
                    $"Los arreglos no tienen la misma cantidad de valores.\n\n" +
                    $"Llegadas: {numeros_Llegada.Length}\n" +
                    $"Servicios: {numeros_Servicio.Length}\n" +
                    $"Diferencia: {diferencia}",
                    "Cantidad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            CargarNumerosEnGrid();
            CargarNumerosEnGrid2();
            CargarNumerosEnGrid3();
            CargaTextoEnLabel();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double valor = (double)numericUpDown1.Value;
            resultado = 1 - (valor / 100);

            if (numeros_Servicio != null)
                textBox2_TextChanged(null, null);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        { 

        }

        private void CargaTextoEnLabel()
        {
            double reduccion2 = 100 - ((100 / tiempoCola1) * tiempoCola2);
            double reduccion3 = 100 - ((100 / tiempoCola1) * tiempoCola3);
            label8.Text = "Con la reduccion del 20% en el tiempo de servicio, el tiempo de cola \n" +
                          " se reduce un: " + reduccion2 + "% con respecto a los datos originales";

            if (tiempoCola1 > tiempoCola3)
            {
                label9.Text = "Con la reduccion personalizada en el tiempo de servicio, el tiempo de cola \n" +
                              " se reduce un: " + reduccion3 + "% con respecto a los datos originales";
            }
            else
            {
                label9.Text = "Con la reduccion personalizada en el tiempo de servicio, el tiempo de cola \n" +
                              " aumenta con respecto a los datos originales \n" +
                              "!No se recomienda esta opción!";
            }
        }
    }
}
