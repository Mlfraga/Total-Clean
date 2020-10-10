using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace totalClean
{
    class CadGastos
    {
        public int id { get; set; }
        public String nome { get; set; }
        public String descricao { get; set; }
        public DateTime dataVencimento { get; set; }
        public double valor { get; set; }
        public String formaPagamento { get; set; }
        public Boolean pago { get; set; }
    }
}
