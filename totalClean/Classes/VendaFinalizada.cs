using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace totalClean
{
    class VendaFinalizada
    {
        public int idVenda { get; set; }
        public Boolean frotista { get; set; }
        public String cliente { get; set; }
        public String telefone { get; set; }
        public String carro { get; set; }
        public String placa { get; set; }
        public String servico { get; set; }
        public Double preco { get; set; }
        public DateTime data { get; set; }
        public Boolean pago { get; set; }
    }
}
