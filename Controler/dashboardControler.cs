using System.Threading.Tasks;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class dashboardControler
    {
        public async Task<KpiSnapshot> GetKpiSnapshotAsync()
        {
            return await Task.Run(() =>
            {
                dashboardModel model = new dashboardModel();
                return model.GetKpis();
            });
        }
    }
}
