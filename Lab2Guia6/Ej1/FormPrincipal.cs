﻿using System;
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

            /*listado con fallos*/
            List<string> lista = new List<string>
            {
                "dni; Apellido, Nombre; telefono; email; monto",
                "25.655.458;Gales Ernestina ;343 - 62775; ernestina.g @gmail.com; 323,2",
                "40 675 458;Herrera, Agustina; (343)6274575; agustina.s@gmail.com; 9000,2",
                "256-4n5A8;Lorenzo, Jorgelina; 1161116127; jorgel - ina@gmail.com;  3232,2",
                "256458;Bushman, Jorleina; 1161117a7127; jorgelina@gmail.com; null",
                "2506458;Menfis, Marciano Laureado; 11611177127; @gmail.com;",
                "2706458;Yamamoto, Pedro ;11611177127 ; yama.que.yama@gmail.com;234.234",
                "2106458;Mimoto, Mariano; 11611177127 ; mariano@;234.234",
                "3406454;Artigas, José Gervasio; 11611177127 ; mariano@;234.234",
                "4446452;Roca, Julio Argentino; 11611177127 ; roca@;",
                "3506452;Perón, Juand Domingo; 11611177127 ; juanito@;234.234"
            };


            /*listado sin con fallos
            List<string> lista = new List<string>
            {
                "dni; Apellido, Nombre; telefono; email; monto",
                "25.655.458;Gales Ernestina ;343 - 6425775; ernestina.g@gmail.com; 323,2",
                "40 675 458;Herrera, Agustina; (343)6274575; agustina.s@gmail.com; 9000,2",
                "2506458;Lorenzo, Jorgelina; 1161116127; jorgelina@gmail.com;  3232,2",
                "2564058;Bushman, Jorgelina; 1161117127; jorgelinab@gmail.com; 1253,88",
                "2506458;Menfis, Marciano Laureado; 1161117127; marcianito@gmail.com;9934,234",
                "2706458;Yamamoto, Pedro ;1161117727 ; yama.que.yama@gmail.com;234,234",
                "2106458;Mimoto, Mariano; 1161177127 ; mariano@gmail.com;234,234",
                "3406454;Artigas, José Gervasio; 1161177127 ; mariano@yahoo.com.ar;234,234",
                "4446452;Roca, Julio Argentino; 1161177127 ; roca@yahoo.com.ar; 2934,234",
                "3506452;Perón, Juand Domingo; 1161177127 ; juanito@frp.org;234,234"
            };
             */

            bool ok = true;
            for (int linea = 1; linea < lista.Count; linea++)
            {
                string[] campos = lista[linea].Split(';');

                bool tieneCantCampos = campos.Length == 5;
                if (tieneCantCampos == true)
                {
                    string dni = campos[0];
                    string apellidoYNombre = campos[1];
                    StringBuilder telefono = new StringBuilder(campos[2]);
                    string email = campos[3];
                    string monto = campos[4];

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

                    #region normalización y validación del APELLIDO Y NOMBRE
                    //normalizar apellido y nombre.
                    apellidoYNombre = apellidoYNombre.Trim().ToUpper();
                    //
                    //verificar longitud nombre >0
                    bool esLongitudApellidoYNombre = apellidoYNombre.Length > 0;
                    //
                    //verificar si solo hay caracteres numéricos
                    bool tieneChrValidosApellidoYNombre = true;
                    string msgChrNoValidosApellidoYNombre = "";
                    for (int idx = 0; idx < apellidoYNombre.Length; idx++)
                    {
                        bool esValido = Char.IsLetter(apellidoYNombre[idx]) || apellidoYNombre[idx] == ',' || apellidoYNombre[idx] == ' '; ; 
                        tieneChrValidosApellidoYNombre &= esValido;
                        if (esValido == false)
                            msgChrNoValidosApellidoYNombre += $"{{pos:{idx + 1}, char:{{ {apellidoYNombre[idx]}}} }},";
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
                    bool verificaComa = camposMonto.Length <= 2;
                    //
                    //verificar si solo hay caracteres numéricos
                    bool tieneChrValidosMonto = true;
                    string msgChrNoValidosMonto = "";
                    for (int idx = 0; idx < monto.Length; idx++)
                    {
                        bool esValido = Char.IsNumber(monto[idx]) || monto[idx] == ',';

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

                        if (esLongitudApellidoYNombre == false)
                            lbResultados.Items.Add($"\t APELLIDOYNOMBRE: longitud:{apellidoYNombre.Length}, esperado mayor a 0 digitos.");
                        if (tieneChrValidosApellidoYNombre == false)
                            lbResultados.Items.Add($"\t APELLIDOYNOMBRE: {msgChrNoValidosApellidoYNombre}");

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
                        
                        ok &= false;
                    }
                }
                else
                {
                    lbResultados.Items.Add($"Linea {linea+1}: Error!");
                    lbResultados.Items.Add($"\t CADENA DE ENTRADA: {{{lista[linea]}}}");
                    lbResultados.Items.Add($"\t CANTIDAD DE CAMPOS: {campos.Length}, es menor a la cantidad esperada(4).");
                    ok &= false;
                }
            }
            if (ok)
                lbResultados.Items.Add($"ok! listado sin errores");
        }
    }
}
