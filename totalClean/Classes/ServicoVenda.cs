﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace totalClean
{
    class ServicoVenda
    {
        public int idVenda { get; set; }
        public Boolean frotista { get; set; }
        public String cliente { get; set; }
        public String CpfCnpj { get; set; }
        public String carro { get; set; }
        public String placa { get; set; }
        public String servico { get; set; }
        public Double preco { get; set; }
        public Double valorCobrado { get; set; }
        public String data { get; set; }
        public Boolean pago { get; set; }
        public String formaPagamento { get; set; }
        public int idVendasServicos { get; set; }
    }
}
