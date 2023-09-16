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
            lbResultados.Items.Clear();

            List<string> lista = new List<string>
            {
                "dni; telefono; email; monto",
                "25.655.458; 343 - 62775; ernestina.m @gmail.com; 323,2",
                "40 675 458; (343)6274575; agustina.s@gmail.com; 9000,2",
                "256-4n5A8; 1161116127; jorgel - ina@gmail.com;  3232,2",
                "256458; 1161117a7127; jorgelina@gmail.com; null",
                "2506458; 11611177127; @gmail.com;",
                "2706458; 11611177127 ; mariano.campos@gmail.com;234.234",
                "2106458; 11611177127 ; mariano@;234.234",
                "3406454; 11611177127 ; mariano@;234.234",
                "4446452; 11611177127 ; mariano@",
                " 3506452; 11611177127 ; mariano@;234.234"
            };

            for (int linea = 1; linea < lista.Count; linea++)
            {
                string[] campos = lista[linea].Split(';');

                bool tieneCantCampos = campos.Length == 4;
                if (tieneCantCampos == true)
                {
                    string dni = campos[0];
                    StringBuilder telefono = new StringBuilder(campos[1]);
                    string email = campos[2];
                    string monto = campos[3];

                    #region normalización y validación del DNI
                    //normalizar dni.
                    dni = dni.Replace(".", "").Replace(" ", "");
                    //
                    //verificar longitud dni
                    bool esLongitudDNI = dni.Length == 7 || dni.Length == 8;
                    //
                    //verificar si solo hay caracteres numéricos
                    bool tieneChrValidosDNI = true;
                    string msgChrNoValidosDNI = "";
                    for (int idx = 0; idx < dni.Length; idx++)
                    {
                        bool esValido = Char.IsNumber(dni[idx]);
                        tieneChrValidosDNI &= esValido;
                        if (esValido == false)
                            msgChrNoValidosDNI += $"{{pos:{idx+1}, char:{{ {dni[idx]}}} }},";
                    }
                    //
                    #endregion

                    #region normalización y validación del TELEFONO
                    //normalizar telefono
                    telefono = telefono.Replace(".", "").Replace(" ", "").
                                        Replace("(", "").Replace(")", "").
                                        Replace("-", "");
                    //
                    //verificar longitud telefono
                    bool esLongitudTel = telefono.Length == 10;
                    //
                    //verificar si solo hay caracteres numéricos
                    bool tieneChrValidosTel = true;
                    StringBuilder msgChrNoValidosTel = new StringBuilder();
                    for (int idx = 0; idx < telefono.Length; idx++)
                    {
                        bool esValido = Char.IsNumber(telefono[idx]);
                        tieneChrValidosTel &= esValido;
                        if (esValido == false)
                            msgChrNoValidosTel.Append($"{{pos:{idx+1}, char:{{{telefono[idx]}}} }},");
                    }
                    //
                    #endregion

                    #region normalización y validación del EMAIL
                    //normalizar email
                    email = email.Trim();
                    //
                    //verificar ocurrencias de @ ==1
                    string[] camposEmail = email.Split('@');
                    bool verificaArroba = camposEmail.Length == 2;
                    //
                    string usr = camposEmail[0];
                    bool esLongitudUsr = usr.Length > 0;
                    //
                    //caracter inicial y otros caracteres del usuario
                    bool tieneChrValidosUsr = true;
                    string msgChrNoValidosUsr = "";
                    for (int idx = 0; idx < usr.Length; idx++)
                    {
                        bool esValido = Char.IsLetterOrDigit(usr[idx]) || usr[idx] == '.';

                        //verificando caracter inicial del campo usuario
                        if (idx == 0) esValido &= Char.IsNumber(usr[idx]) == false;

                        tieneChrValidosUsr &= esValido;
                        if (esValido == false)
                            msgChrNoValidosUsr += $"{{pos:{idx+1}, char:{{ {usr[idx]}}} }},";
                    }
                    //
                    string dom = camposEmail[1];
                    bool esLongitudDom = dom.Length > 0;
                    //
                    ////caracter inicial y otros caracteres del dominio
                    bool tieneChrValidosDom = true;
                    string msgChrNoValidosDom = "";
                    for (int idx = 0; idx < dom.Length; idx++)
                    {
                        bool esValido = Char.IsLetterOrDigit(dom[idx]) || dom[idx] == '.';

                        //verificando caracter inicial del campo dominio
                        if (idx == 0) esValido &= Char.IsNumber(dom[idx]) == false;

                        tieneChrValidosDom &= esValido;
                        if (esValido == false)
                            msgChrNoValidosDom += $"{{pos:{idx+1}, char:{{{dom[idx]}}} }}, ";
                    }
                    #endregion

                    #region normalización y validación del MONTO
                    //normalizar monto
                    monto = monto.Trim();
                    //
                    //verificar ocurrencias de ',' <=1
                    string[] camposMonto = monto.Split(',');
                    bool verificaComa = camposMonto.Length <= 1;
                    //
                    //verificar si solo hay caracteres numéricos
                    bool tieneChrValidosMonto = true;
                    string msgChrNoValidosMonto = "";
                    for (int idx = 0; idx < monto.Length; idx++)
                    {
                        bool esValido = Char.IsNumber(monto[idx]) || monto[idx] != ',';

                        tieneChrValidosMonto &= esValido;
                        if (esValido == false)
                            msgChrNoValidosMonto += $"{{pos:{idx+1}, char:{{ {monto[idx]} }} }}";
                    }
                    #endregion

                    if (esLongitudDNI == false || tieneChrValidosDNI == false ||
                            esLongitudTel == false || tieneChrValidosTel == false ||
                            verificaArroba == false ||
                            esLongitudUsr == false || tieneChrValidosUsr == false ||
                            esLongitudDom == false || tieneChrValidosDom == false)
                    {
                        lbResultados.Items.Add($"Linea {linea+1}: Error!");
                        lbResultados.Items.Add($"\t CADENA DE ENTRADA: {{{lista[linea]}}}");

                        if (esLongitudDNI == false)
                            lbResultados.Items.Add($"\t DNI: longitud:{dni.Length}, esperado entre 7 y 8 digitos.");
                        if (tieneChrValidosDNI == false)
                            lbResultados.Items.Add($"\t DNI: {msgChrNoValidosDNI}");

                        if (esLongitudTel == false)
                            lbResultados.Items.Add($"\t TELEFONO: longitud:{telefono.Length}, esperado 10 digitos.");
                        if (tieneChrValidosTel == false)
                            lbResultados.Items.Add($"\t TELEFONO: usuario: {msgChrNoValidosTel}");

                        //
                        if (verificaArroba == false)
                            lbResultados.Items.Add($"\t EMAIL: Formato no válido.");
                        if (esLongitudUsr == false)
                            lbResultados.Items.Add($"\t EMAIL: Formato del usuario no válido.");
                        if (tieneChrValidosUsr == false)
                            lbResultados.Items.Add($"\t EMAIL: {msgChrNoValidosUsr}");
                        //
                        if (esLongitudDom == false)
                            lbResultados.Items.Add($"\t EMAIL: Formato del dominio no válido.");
                        if (tieneChrValidosDom == false)
                            lbResultados.Items.Add($"\t EMAIL: dominio:{msgChrNoValidosDom}");

                        //
                        if (verificaComa == false)
                            lbResultados.Items.Add($"\t MONTO: Formato no válido, la coma es separador decimal.");
                        if (tieneChrValidosMonto == false)
                            lbResultados.Items.Add($"\t MONTO: {msgChrNoValidosMonto}");
                    }
                }
                else
                {
                    lbResultados.Items.Add($"Linea {linea+1}: Error!");
                    lbResultados.Items.Add($"\t CADENA DE ENTRADA: {{{lista[linea]}}}");
                    lbResultados.Items.Add($"\t CANTIDAD DE CAMPOS: {campos.Length}, es menor a la cantidad esperada(4).");
                }
            }
        }
    }
}
