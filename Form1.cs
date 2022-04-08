using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio_Guía_8
{
    public partial class Form1 : Form
    {

        int xo, yo, tam; //Variables para el valor inicial de x, y y del tamaño
        bool ec = false; //Bandera que más adelante se usara
        bool estado = false; //Otra bandera más adeltante usada
        int n = 0, i = 1;
        //i es e nivel
        //n son los numeros de nodos
        int[] arreglo_numeros; //Arreglo para los números ingresados
        Button[] Arreglo; //Arreglo de boton para simular los valores ingresados


        //==========================================================================================//
        //                 Método que intercambiara valores de heap de números                      //
        public void HPN()
        {
            int temp; //Variable temporal
            int x = arreglo_numeros.Length; //Longitud del arreglo de los valores ingresados

            for (int i = (x - 1); i >= 1; i--) //Desde un valor menos de la longitud total se decrementa
            {
                intercambio(ref Arreglo, i, 1); //Se hace el intercambio
                temp = arreglo_numeros[i]; //El elemento i del arreglo pasa a la variable temporal
                arreglo_numeros[i] = arreglo_numeros[1]; //El elemento 1 pasa a la posición i
                arreglo_numeros[1] = temp; //El que estaba en temporal pasa a la posición 1
                x--;
            }
        }

        //==========================================================================================//
        //                        Método para hacer el intercambio                                  //
        public void intercambio(ref Button[] botones, int a, int b)
        {
            string temp = botones[a].Text; //Dejando los valores en una variable temporal

            Point pa = botones[a].Location; //Sacando la ubicación de a
            Point pb = botones[b].Location; //Sacando la ubicación de b

            int diferenciaX = Math.Abs(pa.X - pb.X); //Sacamos la distancia entre sus X´s
            int diferenciaY = Math.Abs(pa.Y - pb.Y); //Sacamos la distancia entre sus Y´s

            int x = 10;
            int y = 10;
            int t = 70;

            while (y != diferenciaY + 10) //Mientras no se llegue a la posición esperada en y
            {
                Thread.Sleep(t); //Suspendiendo durante 70 milisegundos
                botones[a].Location += new Size(0, -10); //Moviendo "a" -10
                botones[b].Location += new Size(0, +10); //Moviendo "b" +10
                y += 10;
            }

            while (x != diferenciaX - diferenciaX % 10 + 10) //Mientras no lluegue a la posición esperada en x
            {
                if (pa.X < pb.X) //Si x de a es menor que x de b
                {
                    Thread.Sleep(t); //Suspendiendo durante 70 milisegundos
                    botones[a].Location += new Size(+10, 0); //Movemos +10 a "a"
                    botones[b].Location += new Size(-10, 0); //Movemos -10 a "b"
                    x += 10;
                }
                else
                {
                    Thread.Sleep(t);
                    botones[a].Location += new Size(-10, 0);
                    botones[b].Location += new Size(+10, 0);
                    x += 10;
                }

            }

            botones[a].Text = botones[b].Text; //El valor de b se muestra en a
            botones[b].Text = temp; //El valor temporal almacenado se mostrará en b
            botones[b].Location = pb; //Nuevo pb, se almacenará en su ubicación
            botones[a].Location = pa; //Nuevo pa, se va a almacenar en su ubicación
            estado = true;

            tabPage1.Refresh(); //Para refrescar el tabPage

        }
        //==========================================================================================//
        //                        Métodos para dibujar en el tab pages                              //

        //                        Método para dibujar las ramas                                     //
        public void dibujar_Ramas(ref Button[] botones, ref TabPage tb, PaintEventArgs e)
        {
            Pen lapiz = new Pen(Color.Gray, 1.5f);
            Graphics g;
            g = e.Graphics;

            for (int j = 1; j < Arreglo.Length; j++) //Para todos los elementos del arreglo
            {
                if (Arreglo[(2 * j)] != null) //Mientras el arreglo no esté vacio
                    g.DrawLine(lapiz, Arreglo[j].Location.X, Arreglo[j].Location.Y + 20, Arreglo[(2 * j)].Location.X + 20, Arreglo[(2 * j)].Location.Y);
                if (Arreglo[(2 * j) + 1] != null) //Mientra no haya solo un elemento
                    g.DrawLine(lapiz, Arreglo[j].Location.X + 40, Arreglo[j].Location.Y + 20, Arreglo[(2 * j) + 1].Location.X + 20, Arreglo[(2 * j) + 1].Location.Y);
            }

        }

        //                        Método para dibujar el arreglo                                    //
        public void Dibuja_Arreglo(ref Button[] botones, ref TabPage tb)
        {
            for (int j = 1; j < botones.Length; j++) 
            {
                tb.Controls.Add(this.Arreglo[j]);
            }

        }

        //==========================================================================================//
        //                           Método para un Heap Máximizante                                //
        public void Heap_Num()
        {
            ec = true; //Pasando la bandera true

            int x = Arreglo.Length;

            for (int i = ((x) / 2); i > 0; i--) 
                Min_Num(arreglo_numeros, x, i, ref Arreglo);

        }

        //Para un número minimo en heap
        public void Min_Num(int[] a, int x, int indice, ref Button[] botones)
        {
            int izquierdo = (indice * 2);
            int derecho = (indice * 2) + 1;
            int mayor = 0;

            if (izquierdo <x && a[izquierdo] < a[indice])
            {
                mayor = izquierdo;
            }
            else
            {
                mayor = indice;
            }

            if (derecho < x && a[derecho] < a[mayor])
            {
                mayor = derecho;
            }

            if(mayor != indice) // Si el menor es distinto del indice
            {
                int temp = a[indice]; //Valor indice será el temporal
                a[indice] = a[mayor]; //El menor se almacena en la posición del indice
                a[mayor] = temp; //El temporal se almacena en el menor

                intercambio(ref Arreglo, mayor, indice); //Llamando al método intercambio
                Min_Num(a, x, mayor, ref botones); //Llamada recursiva a Max_num

            }

        }

        //Para un número maximo en heap
        public void Max_Num(int[] a, int x, int indice, ref Button[] botones)
        {
            int izquierdo = (indice * 2);
            int derecho = (indice * 2) + 1;
            int mayor = 0;

            if (izquierdo < x && a[izquierdo] < a[indice])
            {
                mayor = izquierdo;
            }
            else
            {
                mayor = indice;
            }

            if (derecho < x && a[derecho] < a[mayor])
            {
                mayor = derecho;
            }

            if (mayor != indice) // Si el menor es distinto del indice
            {
                int temp = a[indice]; //Valor indice será el temporal
                a[indice] = a[mayor]; //El menor se almacena en la posición del indice
                a[mayor] = temp; //El temporal se almacena en el menor

                intercambio(ref Arreglo, mayor, indice); //Llamando al método intercambio
                Max_Num(a, x, mayor, ref botones); //Llamada recursiva a Max_num

            }

        }
        //==========================================================================================//

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(txtNumero.Text);

                if (arreglo_numeros == null)
                {

                    Array.Resize<int>(ref arreglo_numeros, i + 1); //Incrementamos el arreglo en base al nuevo valor ingresado
                    arreglo_numeros[i] = num; //Asignamos ese valor a la posición i del arreglo
                    Array.Resize<Button>(ref Arreglo, i + 1); //Incrementamos el arreglo de botones
                    Arreglo[i] = new Button(); //Creando un nuevo botón i
                    Arreglo[i].Text = arreglo_numeros[i].ToString(); //El texto del botón será el valor ingresado en la posición i 
                    Arreglo[i].Height = 50; //Altura del botón
                    Arreglo[i].Width = 50; //Ancho del botón
                    Arreglo[i].BackColor = Color.FromArgb(222, 17, 84);
                    Arreglo[i].ForeColor = Color.White;
                    Arreglo[i].Location = new Point(xo, yo) + new Size(-20, 0);

                    //Para dibuajr el árbol y crear los niveles se hace
                    if ((i + 1) == Math.Pow(2, n + 1))
                    {
                        n++; //Se incrementa n
                        tam = tam / 2; //Para colocar el nodo justo en medio del tab page
                        xo = tam; //Xo tendrá el mismo valor que el de tam
                        yo += 60; //Incrementando la Y en 60 para que el siguiente nivel se dibuje 60 espacios más abajo
                    }
                    else
                    {
                        xo += (2 * tam); //Si en cierto caso no se cumple lo anterior el nodo se móvera en x
                    }

                    i++; //Incrementando i
                    estado = true;
                    ec = false;
                    tabPage1.Refresh(); //Refrescando el tabPage
                    txtNumero.Clear();
                    txtNumero.Focus();
                    button1_Click_1(null, null);
                }


                else if (arreglo_numeros.Contains<int>(num))
                {
                    MessageBox.Show("Prueba");
                }

                else
                {
                    Array.Resize<int>(ref arreglo_numeros, i + 1); //Incrementamos el arreglo en base al nuevo valor ingresado
                    arreglo_numeros[i] = num; //Asignamos ese valor a la posición i del arreglo
                    Array.Resize<Button>(ref Arreglo, i + 1); //Incrementamos el arreglo de botones
                    Arreglo[i] = new Button(); //Creando un nuevo botón i
                    Arreglo[i].Text = arreglo_numeros[i].ToString(); //El texto del botón será el valor ingresado en la posición i 
                    Arreglo[i].Height = 50; //Altura del botón
                    Arreglo[i].Width = 50; //Ancho del botón
                    Arreglo[i].BackColor = Color.FromArgb(222, 17, 84);
                    Arreglo[i].ForeColor = Color.White;
                    Arreglo[i].Location = new Point(xo, yo) + new Size(-20, 0);

                    //Para dibuajr el árbol y crear los niveles se hace
                    if ((i + 1) == Math.Pow(2, n + 1))
                    {
                        n++; //Se incrementa n
                        tam = tam / 2; //Para colocar el nodo justo en medio del tab page
                        xo = tam; //Xo tendrá el mismo valor que el de tam
                        yo += 60; //Incrementando la Y en 60 para que el siguiente nivel se dibuje 60 espacios más abajo
                    }
                    else
                    {
                        xo += (2 * tam); //Si en cierto caso no se cumple lo anterior el nodo se móvera en x
                    }

                    i++; //Incrementando i
                    estado = true;
                    ec = false;
                    tabPage1.Refresh(); //Refrescando el tabPage
                    txtNumero.Clear();
                    txtNumero.Focus();
                    button1_Click_1(null, null);
                }




            }
            catch { MessageBox.Show("Valor no Válido");}

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limiando la pantalla y reiniciando los valores iniciales
            n = 0;
            i = 1;
            tam = tabPage1.Width / 2;
            xo = tam;
            yo = 20;
            tabPage1.Controls.Clear();
            tabPage1.Refresh();
            Array.Resize<int>(ref arreglo_numeros, 1);
            Array.Resize<Button>(ref Arreglo, 1);
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            if(estado) //Si el estado es verdadero
            {
                try
                {
                    Dibuja_Arreglo(ref Arreglo, ref tabPage1); //Dibuja el arreglo
                    dibujar_Ramas(ref Arreglo, ref tabPage1, e); //Dibujar ramas
                }
                catch { }

                estado = false; //Pasa el estado a falso
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        { 

            if (i == 1)
            {
                MessageBox.Show("No hay elementos que ordenar");
            }

            else
            {
                btnAgregar.Enabled = false;
                btnLimpiar.Enabled = false;
                btnOrdenar.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                if (!ec)
                {
                    Heap_Num(); //Llamando el heap num
                }
                else
                {
                    HPN(); //Llamando el hpn
                }

                //Se habilita todo de nuevo
                btnAgregar.Enabled = true;
                btnLimpiar.Enabled = true;
                btnOrdenar.Enabled = true;
                this.Cursor = Cursors.Default;

            }
        }
        private void btnOrdenar_Click(object sender, EventArgs e)
        {
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           



        }

        public Form1()
        {
            InitializeComponent();
            tam = tabPage1.Width / 2; //tam será la mitad del ancho del tabpage
            xo = tam; //El valor inicial de x será la mitad del ancho del tabpage
            yo = 20; //El valor inicial en y será de 20
            txtNumero.Focus(); //Para hacer un focus
        }

        public void Ordenar_Datos()
        {

            if (i == 1)
            {
                MessageBox.Show("No hay elementos que ordenar");
            }

            else
            {
                btnAgregar.Enabled = false;
                btnLimpiar.Enabled = false;
                btnOrdenar.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                if (!ec)
                {
                    Heap_Num(); //Llamando el heap num
                }
                else
                {
                    HPN(); //Llamando el hpn
                }

                //Se habilita todo de nuevo
                btnAgregar.Enabled = true;
                btnLimpiar.Enabled = true;
                btnOrdenar.Enabled = true;
                this.Cursor = Cursors.Default;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
