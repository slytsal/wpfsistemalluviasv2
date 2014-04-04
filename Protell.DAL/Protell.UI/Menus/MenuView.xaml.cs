using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Protell.ViewModel;
using Protell.Model;

namespace Protell.UI.Menus
{
    /// <summary>
    /// Lógica de interacción para MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl,IContentControl
    {
        public MenuView()
        {
            InitializeComponent();
            this.DataContext = new MenuViewModel();
        }
        private MenuViewModel GetViewModel()
        {
            return this.DataContext as MenuViewModel;
        }
        private void ListSubMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (sender != null)
            {
                ListBox dg = sender as ListBox;
                MenuModel view = dg.SelectedItem as MenuModel;
                if (view != null)                
                   GetCatalogo(view); 
            }
            
        }

        public ContentControl GetContentPane()
        {
            ContentControl cc = null;
            try
            {
                cc = ((Grid)((ContentControl)this.Parent).Parent).FindName("ContentPaneSubMenu") as ContentControl;
            }
            catch (Exception)
            {

                return cc;
            }

            return cc;
        }

        public void GetCatalogo(MenuModel view)
        {
            switch (view.MenuName)
            {
                case "SISTEMAS":
                    Sistema.SistemaView _SistemaView = new Sistema.SistemaView();
                    this.GetContentPane().Content = _SistemaView;
                    break;
                case "CONDICION PROTOCOLO": 
                    CondPro.CondProView _CondProView = new CondPro.CondProView();
                    this.GetContentPane().Content = _CondProView;
                    break;
                case "CONSIDERACION":
                    Consideracion.ConsideracionView _ConsideracionView = new Consideracion.ConsideracionView();
                    this.GetContentPane().Content = _ConsideracionView;
                    break;
                case "DEPENDENCIA": 
                    Dependencia.DependenciaView _DependenciaView = new Dependencia.DependenciaView();
                    this.GetContentPane().Content = _DependenciaView;
                    break;
                case "ESTRUCTURA":
                    Estructura.EstructuraView _EstructuraView = new Estructura.EstructuraView();
                    this.GetContentPane().Content = _EstructuraView;
                    break;
                case "PUNTO MEDICION": 
                    PuntoMedicion.PuntoMedicionView _PuntoMedicionView = new PuntoMedicion.PuntoMedicionView();
                    this.GetContentPane().Content = _PuntoMedicionView;
                    break;
                case "TIPO PUNTO MEDICION": 
                    TipoPuntoMedicion.TipoPuntoMedicionView _TipoPuntoMedicionView = new TipoPuntoMedicion.TipoPuntoMedicionView();
                    this.GetContentPane().Content = _TipoPuntoMedicionView;
                    break;
                case "UNIDAD MEDIDA": 
                    UnidadMedida.UnidadMedidaView _UnidadMedidaView = new UnidadMedida.UnidadMedidaView();
                    this.GetContentPane().Content = _UnidadMedidaView;
                    break;
                case "REGISTRO": 
                    Registro.RegistroView _RegistroView = new Registro.RegistroView();
                    this.GetContentPane().Content = _RegistroView;
                    break;
                case "ACCION PROTOCOLO":
                    AccionProtocolo.AccionProtocoloView _AccionProtocoloView = new AccionProtocolo.AccionProtocoloView();
                    this.GetContentPane().Content = _AccionProtocoloView;
                    break;
                case "ESTRUCTURA PUNTO MEDIO": 
                    EstPuntoMed.EstPuntoMedView _EstPuntoMedView = new EstPuntoMed.EstPuntoMedView();
                    this.GetContentPane().Content = _EstPuntoMedView;
                    break;
                case "ESTRUCTURA DEPENDENCIA": 
                    EstructuraDependencia.EstructuraDependenciaView _EstructuraDependenciaView = new EstructuraDependencia.EstructuraDependenciaView();
                    this.GetContentPane().Content = _EstructuraDependenciaView;
                    break;
                default:
                    break;
            }
        }
    }
}
