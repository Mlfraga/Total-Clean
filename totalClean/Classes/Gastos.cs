﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace totalClean
{
    class Gastos
    {

        public int id { get; set; }
        public String nome { get; set; }
        public String descricao { get; set; }
        public String dataVencimento { get; set; }
        public double valor { get; set; }
        public String formaPagamento { get; set; }
        public Boolean pago { get; set; }

    }
}
