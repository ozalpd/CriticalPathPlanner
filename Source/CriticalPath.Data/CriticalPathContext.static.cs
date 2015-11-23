using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CriticalPathContext
    {
        public async Task<List<CurrencyDTO>> GetCurrencyDtoList()
        {
            if (_currencyDtos == null)
            {
                var list = await GetCurrencyQuery().ToListAsync();
                _currencyDtos = new List<CurrencyDTO>();
                foreach (var item in list)
                {
                    _currencyDtos.Add(new CurrencyDTO(item));
                }
            }

            return _currencyDtos;
        }
        static List<CurrencyDTO> _currencyDtos;

        public async Task RefreshCurrencyDtoList()
        {
            var list = await GetCurrencyQuery().ToListAsync();
            var Currencys = new List<CurrencyDTO>();
            foreach (var item in list)
            {
                Currencys.Add(new CurrencyDTO(item));
            }
            _currencyDtos = Currencys;
        }

        public async Task<List<FreightTermDTO>> GetFreightTermDtoList()
        {
            if (_freightTermDtos == null)
            {
                var list = await GetFreightTermQuery().ToListAsync();
                _freightTermDtos = new List<FreightTermDTO>();
                foreach (var item in list)
                {
                    _freightTermDtos.Add(new FreightTermDTO(item));
                }
            }

            return _freightTermDtos;
        }
        static List<FreightTermDTO> _freightTermDtos;

        public async Task RefreshFreightTermDtoList()
        {
            var list = await GetFreightTermQuery().ToListAsync();
            var FreightTerms = new List<FreightTermDTO>();
            foreach (var item in list)
            {
                FreightTerms.Add(new FreightTermDTO(item));
            }
            _freightTermDtos = FreightTerms;
        }

        public async Task<List<SizingStandardDTO>> GetSizingStandardDtoList()
        {
            if (_sizingStandardDtos == null)
            {
                var list = await GetSizingStandardQuery().ToListAsync();
                _sizingStandardDtos = new List<SizingStandardDTO>();
                foreach (var item in list)
                {
                    _sizingStandardDtos.Add(new SizingStandardDTO(item));
                }
            }

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
