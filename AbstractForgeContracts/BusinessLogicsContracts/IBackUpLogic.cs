using AbstractForgeContracts.BindingModels;

namespace AbstractForgeContracts.BusinessLogicsContracts
{
    public interface IBackUpLogic
    {
        void CreateBackUp(BackUpSaveBindingModel model);
    }
}
