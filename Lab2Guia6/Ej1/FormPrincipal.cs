using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ej1
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            List<string> lista = new List<string>
            {
                "dni; telefono; email",
                "25.655.458; 343 - 62775; ernestina.m @gmail.com",
                "40 675 458; (343)6274575; agustina.s@gmail.com",
                "256-4n5A8; 1161116127; jorgel - ina@gmail.com",
                "256458; 1161117a7127; jorgelina@gmail.com",
            };

            for (int linea = 1; linea < lista.Count; linea++)
            {
                string[] campos = lista[linea].Split(';');

                string dni = campos[0];
                StringBuilder telefono = new StringBuilder(campos[1]);
                string email = campos[2];

                //normalizar dni.
                dni=dni.Replace(".", "").Replace(" ", "");
                //verificar longitud dni
                bool esLongitudDNI = dni.Length == 7 || dni.Length == 8;
                //verificar si solo hay caracteres numéricos
                bool tieneChrValidosDNI=true;
                string msgChrNoValidosDNI = "";
                for(int idx=0;idx<dni.Length; idx++)
                {
                    bool esValido= Char.IsNumber(dni[idx]);
                    tieneChrValidosDNI &= esValido;
                    if (esValido==false)
                        msgChrNoValidosDNI += $"{{pos:{idx}, char:{dni[idx]}}}";
                }
                //

                //normalizar telefono
                telefono = telefono.Replace("(","").Replace(")", "").Replace("-", "").Replace(" ", "");
                bool esLongitudTel = telefono.Length == 10 || telefono.Length == 10;
                #region verificar si solo hay caracteres numéricos
                bool tieneChrValidosTel = true;
                StringBuilder msgChrNoValidosTel = new StringBuilder("");
                for (int idx = 0; idx < telefono.Length; idx++)
                {
                    bool esValido = Char.IsNumber(telefono[idx]);
                    tieneChrValidosTel &= esValido;
                    if (esValido==false)
                        msgChrNoValidosTel.Append( $"{{pos:{idx}, char:{telefono[idx]}}} " );
                }
                #endregion

                //falta el email

                
                if (esLongitudDNI == false || tieneChrValidosDNI == false ||
                        esLongitudTel == false || tieneChrValidosTel == false)
                {
                    listBox1.Items.Add($"Linea {linea}: error!");

                    if(esLongitudDNI==false)
                        listBox1.Items.Add($"\t DNI:valor esperado entre 7 y 8 digitos");
                    if (tieneChrValidosDNI == false)
                        listBox1.Items.Add($"\t DNI: {msgChrNoValidosDNI}");

                    if (esLongitudTel == false)
                        listBox1.Items.Add($"\t TELEFONO: valor esperado entre 10 digitos");
                    if (tieneChrValidosTel == false)
                        listBox1.Items.Add($"\t TELEFONO: {msgChrNoValidosTel}");

                    //terminar.
                }
            }
        }
    }
}
