﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_WPF.Models
{
    public class ProfessorList : ObservableCollection<Professors>
    {
        public ProfessorList() : base()
        {
           
        }
    }
}
