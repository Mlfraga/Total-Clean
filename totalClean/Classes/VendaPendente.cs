using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace totalClean
{
    class VendaPendente
    {
        public int idVenda { get; set; }
        public Boolean frotista { get; set; }
        public String cliente { get; set; }
        public String CpfCnpj { get; set; }
        public String carro { get; set; }
        public String placa { get; set; }
        public DateTime data { get; set; }
        public Boolean pago { get; set; } 
    }
}
