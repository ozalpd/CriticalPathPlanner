using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CriticalPathContext
    {
        public async Task<List<CountryDTO>> GetCountryDtoList()
        {
            if (_countryDtos == null)
            {
                var list = await GetCountryQuery().ToListAsync();
                _countryDtos = new List<CountryDTO>();
                foreach (var item in list)
                {
                    _countryDtos.Add(new CountryDTO(item));
                }
            }

            return _countryDtos;
        }
        static List<CountryDTO> _countryDtos;

        public async Task RefreshCountryDtoList()
        {
            var list = await GetCountryQuery().ToListAsync();
            var Countrys = new List<CountryDTO>();
            foreach (var item in list)
            {
                Countrys.Add(new CountryDTO(item));
            }
            _countryDtos = Countrys;
        }


        public async Task<List<CurrencyDTO>> GetCurrencyDtoList()
        {
            if (_currencyDtos == null)
                await RefreshCurrencyDtoList();

            return _currencyDtos;
        }
        static List<CurrencyDTO> _currencyDtos;

        public async Task RefreshCurrencyDtoList()
        {
            var list = await GetCurrencyDtoQuery().ToListAsync();
            _currencyDtos = list;
        }

        public async Task<List<EmployeePositionDTO>> GetEmployeePositionDtoList()
        {
            if (_employeePositionDtos == null)
                await RefreshEmployeePositionDtoList();

            return _employeePositionDtos;
        }
        static List<EmployeePositionDTO> _employeePositionDtos;

        public async Task RefreshEmployeePositionDtoList()
        {
            var list = await GetEmployeePositionDtoQuery().ToListAsync();
            _employeePositionDtos = list;
        }

        public async Task<List<EmployeeDTO>> GetDesignerDtoList()
        {
            if (_designerDtos == null)
            {
                await RefreshDesignerDtoList();
            }

            return _designerDtos;
        }
        static List<EmployeeDTO> _designerDtos;

        public async Task RefreshDesignerDtoList()
        {
            var query = GetEmployeeQuery()
                        .Where(e => e.PositionId == (int)DefaultJobPositions.Designer);

            var list = await GetEmployeeDtoQuery(query).ToListAsync();
            _designerDtos = list;
        }

        public async Task<List<EmployeeDTO>> GetMerchandiserDtoList()
        {
            if (_merchandiserDtos == null)
            {
                await RefreshMerchandiserDtoList();
            }

            return _merchandiserDtos;
        }
        static List<EmployeeDTO> _merchandiserDtos;

        public async Task RefreshMerchandiserDtoList()
        {
            var query = GetEmployeeQuery()
                        .Where(e => e.PositionId == (int)DefaultJobPositions.Merchandiser);

            var list = await GetEmployeeDtoQuery(query).ToListAsync();
            _merchandiserDtos = list;
        }

        public async Task<List<FreightTermDTO>> GetFreightTermDtoList()
        {
            if (_freightTermDtos == null)
                 _freightTermDtos = await GetFreightTermDtoQuery().ToListAsync();

            return _freightTermDtos;
        }
        static List<FreightTermDTO> _freightTermDtos;

        public async Task RefreshFreightTermDtoList()
        {
            var list = await GetFreightTermDtoQuery().ToListAsync();
            _freightTermDtos = list;
        }

        public async Task<List<SizingStandardDTO>> GetSizingStandardDtoList()
        {
            if (_sizingStandardDtos == null)
                await RefreshSizingStandardDtoList();

            return _sizingStandardDtos;
        }
        static List<SizingStandardDTO> _sizingStandardDtos;

        public async Task RefreshSizingStandardDtoList()
        {
            var list = await GetSizingStandardQuery().ToListAsync();
            var sizingStandards = new List<SizingStandardDTO>();
            foreach (var item in list)
            {
                sizingStandards.Add(new SizingStandardDTO(item));
            }
            _sizingStandardDtos = sizingStandards;
        }
    }
}
