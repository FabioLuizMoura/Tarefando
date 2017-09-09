using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tarefando.Data.Model;

namespace Tarefando
{
    public class ListViewAdapter : BaseAdapter<Tarefa>
    {
        public List<Tarefa> mTarefas;
        private Context mContext;
        public ListViewAdapter(Context context, List<Tarefa> tarefas)
        {
            mTarefas = tarefas;
            mContext = context;
        }
        public override Tarefa this[int position]
        {
            get
            {
                return mTarefas[position];
            }
        }

        public override int Count
        {
            get
            {
                return mTarefas.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListaDeAfazeres, null, false);
            }

            TextView txtNome = view.FindViewById<TextView>(Resource.Id.txtListaNome);
            txtNome.Text = mTarefas[position].Nome;

            TextView txtData = view.FindViewById<TextView>(Resource.Id.txtListaData);
            txtData.Text = mTarefas[position].DataCriada.ToString();

            return view;

        }
    }
}