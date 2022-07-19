using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VidlyAPI.Models.DTO;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/MemberShipType")]
    [ApiController]
    public class MemberShipTypeController : ControllerBase
    {
        private readonly IMemberShipTypeRepository _memberShipType;
        private readonly IMapper _Imapper;
        public MemberShipTypeController(IMemberShipTypeRepository memberShipType, IMapper mapper)
        {
            _memberShipType = memberShipType;
            _Imapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MemberShipTypeDto>))]
        public IActionResult GetAll()
        {
            var MemberShipeTypes = _memberShipType.GetAllMemberShipTypes();
            var MemberShipTypeDtoList = new List<MemberShipTypeDto>();
            foreach (var MemberShipType in MemberShipeTypes)
            {
                MemberShipTypeDtoList.Add(_Imapper.Map<MemberShipTypeDto>(MemberShipType));
            }

            return Ok(MemberShipTypeDtoList);
        }
    }
}
