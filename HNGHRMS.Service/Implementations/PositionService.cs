using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Repository;
using HNGHRMS.Service.Interface;
namespace HNGHRMS.Service.Implementations
{
    public class PositionService : IPositionService
    {

        private readonly IPositionRepository positionRepository;
        private readonly IUnitOfWork unitOfWork;

        public PositionService(IPositionRepository positionRepository,IUnitOfWork unitOfWork)
        {
            this.positionRepository = positionRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Position> GetPositions()
        {
            return positionRepository.GetAll();
        }

        public Position GetPosition(int Id)
        {
            return positionRepository.GetById(Id);
        }

        public void CreatePostion(Position Position)
        {
            positionRepository.Add(Position);
            SavePosition();
        }

        public void EditPosition(Position Position)
        {
            positionRepository.Update(Position);
            SavePosition();
        }

        public void DeletePosition(Position Position)
        {
            positionRepository.Delete(Position);
            SavePosition();
        }

        public void SavePosition()
        {
            this.unitOfWork.Commit();
        }
    }
}
