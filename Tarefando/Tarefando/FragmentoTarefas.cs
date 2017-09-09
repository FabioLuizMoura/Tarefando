using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Tarefando.Data.Dao;
using Tarefando.Data.Model;

namespace Tarefando
{
    public class FragmentoTarefas : DialogFragment
    {
        private Button btnApagar;
        private Button btnEditar;
        private Button btnConcluir;
        private Button btnDesConcluir;
        private Banco _banco;
        private int id;
        private View view;
        private bool conc;
        private Context contexto;

        public FragmentoTarefas(int id, bool concl, Context con)
        {
            this.id = id;
            _banco = new Banco();
            conc = concl;
            contexto = con;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            base.OnCreateView(inflater, container, savedInstanceState);

            view = inflater.Inflate(Resource.Layout.MenuItens, container, false);

            btnApagar = view.FindViewById<Button>(Resource.Id.btnItensApagar);

            btnEditar = view.FindViewById<Button>(Resource.Id.btnItensEditar);

            btnEditar.Click += BtnEditar_Click;

            btnApagar.Click += BtnApagar_Click;

            if (conc)
            {
                btnConcluir = view.FindViewById<Button>(Resource.Id.btnItensConclui);
                btnConcluir.Click += BtnConcluir_Click;
            }
            else
            {
                btnDesConcluir = view.FindViewById<Button>(Resource.Id.btnItensConclui);
                btnDesConcluir.Text = "Des Concluir";
                btnDesConcluir.Click += BtnDesConcluir_Click;
            }


            return view;
        }

        private void BtnDesConcluir_Click(object sender, EventArgs e)
        {
            var tarefa = _banco.Db.Table<Tarefa>().Where(x => x.ID == id).FirstOrDefault();
            tarefa.Completa = false;
            _banco.Db.Update(tarefa);
            this.Dismiss();
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            var tarefa = _banco.BuscarTarefa(id);
            tarefa.Completa = true;
            _banco.Update(tarefa);
            this.Dismiss();
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                _banco.ExcluirTarefa(id);
                this.Dismiss();
            }
            catch (Exception ex)
            {
                Toast.MakeText(contexto, ex.Message, ToastLength.Short).Show();
            }

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            var intent = new Intent(contexto, typeof(EditarTarefaActivity));
            intent.PutExtra("id", id);
            StartActivity(intent);
            this.Dismiss();
        }
    }
}