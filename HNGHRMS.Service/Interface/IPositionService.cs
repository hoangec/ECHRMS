using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Model.Models;
namespace HNGHRMS.Service.Interface
{
    public interface IPositionService
    {
        IEnumerable<Position> GetPositions();
        Position GetPosition(int Id);
        void CreatePostion(Position Position);
        void EditPosition(Position Position);
        void DeletePosition(Position Position);
        void SavePosition();
    }
}
