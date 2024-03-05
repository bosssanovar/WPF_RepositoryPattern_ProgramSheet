using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public class SpeakerOnOffDetailEntity : EntityBase<SpeakerOnOffDetailEntity>
    {
        public List<SpeakerOnOffVO> SpeakerOnOffDetail { get; set; } = [];

        public void Init(int count)
        {
            SpeakerOnOffDetail.Clear();
            for (int i = 0; i < count; i++)
            {
                SpeakerOnOffDetail.Add(new(i % 3 == 0));
            }
        }
    }
}
