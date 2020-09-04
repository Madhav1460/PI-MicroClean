using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Commands
{
    public class CreateOptionCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsMatched { get; set; }
    }
}
