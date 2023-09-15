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
                "2506458; 11611177127; @gmail.com",
                "2506458; 11611177127; mariano@",
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
                //verificar longitud telefono
                bool esLongitudTel = telefono.Length == 10 || telefono.Length == 10;
                //verificar si solo hay caracteres numéricos
                bool tieneChrValidosTel = true;
                StringBuilder msgChrNoValidosTel = new StringBuilder();
                for (int idx = 0; idx < telefono.Length; idx++)
                {
                    bool esValido = Char.IsNumber(telefono[idx]);
                    tieneChrValidosTel &= esValido;
                    if (esValido==false)
                        msgChrNoValidosTel.Append( $"{{pos:{idx}, char:{telefono[idx]}}} " );
                }
                //

                email = email.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                //verificar ocurrencias de @ ==1
                string[] camposEmail = email.Split('@');
                bool verificaArroba = camposEmail.Length == 2;
                //
                string usr = camposEmail[0];
                bool esLongitudUsr = usr.Length > 0;
                //
                bool tieneChrValidosUsr = true;
                string msgChrNoValidosUsr = "";
                for (int idx = 0; idx < usr.Length; idx++)
                {
                    bool esValido = usr[idx]!='+' && usr[idx] != '-' && usr[idx] != '\\';
                    if (idx == 0) esValido &= Char.IsNumber(usr[idx]) == false;

                    tieneChrValidosUsr &= esValido;
                    if (esValido == false)
                        msgChrNoValidosUsr += $"{{pos:{idx}, char:{usr[idx]}}} ";
                }
                //
                string dom = camposEmail[1];
                bool esLongitudDom = dom.Length > 0;
                //
                bool tieneChrValidosDom = true;
                string msgChrNoValidosDom = "";
                for (int idx = 0; idx < dom.Length; idx++)
                {
                    bool esValido = dom[idx] != '+' && dom[idx] != '-' && dom[idx] != '\\';
                    if (idx == 0) esValido &= Char.IsNumber(dom[idx]) == false;

                    tieneChrValidosDom &= esValido;
                    if (esValido == false)
                        msgChrNoValidosDom += $"{{pos:{idx}, char:{dom[idx]}}} ";
                }
                //

                if (esLongitudDNI == false || tieneChrValidosDNI == false ||
                        esLongitudTel == false || tieneChrValidosTel == false ||
                        verificaArroba==false||
                            esLongitudUsr==false || tieneChrValidosUsr==false||
                            esLongitudDom == false || tieneChrValidosDom == false )
                {
                    listBox1.Items.Add($"Linea {linea}: Error! {lista[linea]}.");

                    if(esLongitudDNI==false)
                        listBox1.Items.Add($"\t DNI: Valor esperado entre 7 y 8 digitos.");
                    if (tieneChrValidosDNI == false)
                        listBox1.Items.Add($"\t DNI: {msgChrNoValidosDNI}");

                    if (esLongitudTel == false)
                        listBox1.Items.Add($"\t TELEFONO: Valor esperado entre 10 digitos.");
                    if (tieneChrValidosTel == false)
                        listBox1.Items.Add($"\t TELEFONO: {msgChrNoValidosTel}");

                    //terminar.
                    if (verificaArroba == false)
                        listBox1.Items.Add($"\t EMAIL: Formato no válido.");
                    if (esLongitudUsr == false)
                        listBox1.Items.Add($"\t EMAIL: Formato del usuario no válido.");
                    if (tieneChrValidosUsr == false)
                        listBox1.Items.Add($"\t EMAIL: {msgChrNoValidosUsr}");
                    //
                    if (esLongitudDom == false)
                        listBox1.Items.Add($"\t EMAIL: Formato del dominio no válido.");
                    if (tieneChrValidosDom == false)
                        listBox1.Items.Add($"\t EMAIL: {msgChrNoValidosDom}");
                }
            }
        }
    }
}
