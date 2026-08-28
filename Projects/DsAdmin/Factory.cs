using System;

namespace DsAdmin
{
    public class Factory : IDisposable
    {

        DAO transacao = new DAO();

        public DsAdmin.Arquivo Arquivo()
        {
            return new Arquivo(transacao);
        }

        public DsAdmin.Brasao Brasao()
        {
            return new Brasao(transacao);
        }

        public DsAdmin.Banner Banner()
        {
            return new Banner(transacao);
        }

        public DsAdmin.CadernoModelo CadernoModelo()
        {
            return new CadernoModelo(transacao);
        }

        public DsAdmin.CarimboRegistro CarimboRegistro()
        {
            return new CarimboRegistro(transacao);
        }

        public DsAdmin.Cliente Cliente()
        {
            return new Cliente(transacao);
        }

        public DsAdmin.Diagramador Diagramador()
        {
            return new Diagramador(transacao);
        }

        public DsAdmin.Editoracao Editoracao()
        {
            return new Editoracao(transacao);
        }

        public DsAdmin.Maquina Maquina()
        {
            return new Maquina(transacao);
        }

        public DsAdmin.ModeloImagem ModeloImagem()
        {
            return new ModeloImagem(transacao);
        }

        public DsAdmin.Publicacao Publicacao()
        {
            return new Publicacao(transacao);
        }

        public DsAdmin.FonteModelo FonteModelo()
        {
            return new FonteModelo(transacao);
        }

        public DsAdmin.CamposModelo CamposModelo()
        {
            return new CamposModelo(transacao);
        }

        public DsAdmin.ModeloEdicao ModeloEdicao()
        {
            return new ModeloEdicao(transacao);
        }

        public DsAdmin.StatusPublicacao StatusPublicacao()
        {
            return new StatusPublicacao(transacao);
        }

        public DsAdmin.Revisor Revisor()
        {
            return new Revisor(transacao);
        }

        public DsAdmin.ContasPublicas ContasPublicas()
        {
            return new ContasPublicas(transacao);
        }

        //#Region "IDisposable Support
        private Boolean disposedValue;  //' To detect redundant calls

        //' IDisposable
        protected void Dispose(Boolean disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    //' TODO: dispose managed state (managed objects).
                }

                //' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                //' TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        //' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //'Protected Overrides Sub Finalize()
        //'    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //'    Dispose(False)
        //'    MyBase.Finalize()
        //'End Sub

        public void Commit()
        {
            transacao.Commit();
        }

        //' This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            //' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            //transacao.Commit();
            transacao.Rollback();

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //#End Region

    }
}

