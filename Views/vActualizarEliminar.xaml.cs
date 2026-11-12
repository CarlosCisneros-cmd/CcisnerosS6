using CcisnerosS6.Models;
using System.Net;

namespace CcisnerosS6.Views;

public partial class vActualizarEliminar : ContentPage
{
    private const string Url = "http://192.168.17.15/ws_uisrael/ws_estudiante.php";

    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();

            string urlActualizar = Url +
                "?codigo=" + Uri.EscapeDataString(txtCodigo.Text) +
                "&nombre=" + Uri.EscapeDataString(txtNombre.Text) +
                "&apellido=" + Uri.EscapeDataString(txtApellido.Text) +
                "&edad=" + Uri.EscapeDataString(txtEdad.Text);

            cliente.UploadValues(urlActualizar, "PUT", parametros);
            await DisplayAlertAsync("Alerta", "Estudiante actualizado", "OK");
            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlertAsync("Eliminar",
                "¿Seguro que deseas eliminar a " + txtNombre.Text + " " + txtApellido.Text + "?",
                "Sí", "No");
            if (!confirmar)
                return;

            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();

            string urlEliminar = Url + "?codigo=" + Uri.EscapeDataString(txtCodigo.Text);

            cliente.UploadValues(urlEliminar, "DELETE", parametros);
            await DisplayAlertAsync("Alerta", "Estudiante eliminado", "OK");
            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", ex.Message, "OK");
        }
    }
}