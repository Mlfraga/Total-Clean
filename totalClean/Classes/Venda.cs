using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace totalClean
{
    class Venda
    {
        public int idVenda { get; set; }
        public int idCliente { get; set; }
        public String carro { get; set; }
        public String placa { get; set; }
        public DateTime data { get; set; }
        public String formaPagamento { get; set; }
    }
}
