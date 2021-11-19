using Microsoft.AspNetCore.Mvc;
using SudokuSolver.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver.ASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<List<int>>> Solve() =>
            Services.SudokuService.Solve();

        [HttpGet("{data}")]
        public ActionResult<bool> Get(string data)
        {
            if (data.Length != 81) return false;
            var result = true;
            var dataTable = new int[9, 9];

            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    result &= int.TryParse("" + data[i * 9 + j], out dataTable[i, j]);
                }
            }

            result &= Services.SudokuService.InsertaData(dataTable);
            

            return result;
        }
    }
}
