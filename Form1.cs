using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
using System.Windows.Forms;
using Aga.Models;
using Aga.Presentation;

namespace Aga
{
    public partial class Form1 : Form
    {
        GetId gi = new GetId();
        GetIdNum idN = new GetIdNum();

        public Form1()
        {
            InitializeComponent();
        }
        public class GetPersona
        {
            public int IdPersona { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Correo { get; set; }
            public int Edad { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public bool Activo { get; set; }
        }
        public class GetNumero
        {
            public int IdNumero { get; set; }
            public string Numero { get; set; }
            public int IdPersona { get; set; }
            public int IdTipo { get; set; }
        }

        public class GetId
        {
            public int iPersona { get; set; }
        }
        public class GetIdNum
        {
            public int idNumber { get; set; }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            
            using (AgaTestEntities1 db = new AgaTestEntities1())
            {
                try
                {
                    List<GetPersona> List = new List<GetPersona>();
                    string query = "SELECT IdPersona,Nombre,ApellidoPaterno,ApellidoMaterno,Correo,Edad,FechaNacimiento,Activo FROM Persona";
                    List<GetPersona> result = db.Database.SqlQuery<GetPersona>(query).ToList();              
                    dataGridView1.DataSource = result.ToList();
                    List<GetNumero> List1 = new List<GetNumero>();
                    string query1 = "SELECT * FROM Numero";
                    List<GetNumero> result1 = db.Database.SqlQuery<GetNumero>(query1).ToList();
                    dataGridView2.DataSource = result1.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }   
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Presentation.FormUno agregar = new Presentation.FormUno();
            agregar.ShowDialog();
            Reload();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Presentation.FormTelefono tel = new Presentation.FormTelefono();
            tel.ShowDialog();
        }
        /// <summary>
        /// Obtiene ID de la persona
        /// </summary>
        /// <returns></returns>
        public object getIdPersona()
        {
            var Id1 = 0;
            FormTelefono telefono = new FormTelefono();
            try
            {
                var count = dataGridView1.SelectedRows.Count;
                if(dataGridView1.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                    var obj = row.Cells["IdPersona"].Value;
                    gi.iPersona = Id1 = Convert.ToInt32(obj);
                }
                return gi.iPersona;
            }
            catch (Exception ee)
            {
                Console.Write("error" + ee.Message);
                return 0;
            }

        }
        /// <summary>
        /// Recarga tablas
        /// </summary>
        public void Reload()
        {
            using (AgaTestEntities1 db = new AgaTestEntities1())
            {
                try
                {
                    List<GetPersona> List = new List<GetPersona>();
                    string query = "SELECT IdPersona,Nombre,ApellidoPaterno,ApellidoMaterno,Correo,Edad,FechaNacimiento,Activo FROM Persona";
                    List<GetPersona> result = db.Database.SqlQuery<GetPersona>(query).ToList();
                    dataGridView1.DataSource = result.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void ReloadNumber()
        {
            using (AgaTestEntities1 db = new AgaTestEntities1())
            {
                try
                {
                    List<GetNumero> List = new List<GetNumero>();
                    string query = "SELECT * FROM Numero";
                    List<GetNumero> result = db.Database.SqlQuery<GetNumero>(query).ToList();
                    dataGridView2.DataSource = result.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
            
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            


        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (AgaTestEntities1 db = new AgaTestEntities1())
            {
                try
                {
                    getIdPersona();
                    //Inserción de datos de la persona
                    Numero numero1 = new Numero
                    {
                        IdPersona = gi.iPersona,
                        Numero1 = textNumero.Text,
                        IdTipo = comboBox1.SelectedIndex == 0 ? 1 : 2
                    };
                    db.Numero.Add(numero1);
                    db.SaveChanges();
                    MessageBox.Show("Numero ingresado");
                    ReloadNumber();
                    //this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Error al ingresar numero");
                    throw;
                }
                finally
                {
                    textNumero.Text = String.Empty;
                    // this.Close();
                }
            }
        }
        public object GetIdNumero()
        {
            try
            {
                var IdNumber = 0;
                var count = dataGridView2.SelectedRows.Count;
                if (dataGridView2.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = this.dataGridView2.SelectedRows[0];
                    var obj = row.Cells["IdNumero"].Value;
                    idN.idNumber = IdNumber = Convert.ToInt32(obj);
                }
                return idN.idNumber;
            }
            catch (Exception ee)
            {
                Console.Write("error" + ee.Message);
                return 0;
            }
        }
        /// <summary>
        /// Elimina numero de persona
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            using (AgaTestEntities1 db = new AgaTestEntities1())
            {
                try
                {
                    GetIdNumero();
                    //Inserción de datos de la persona
                    Numero delete = db.Numero.Find(idN.idNumber);
                    db.Numero.Remove(delete);
                    db.SaveChanges();
                    MessageBox.Show("Datos borrados");
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Datos borrados");
                    throw;
                }

            }

        }
    }
}
