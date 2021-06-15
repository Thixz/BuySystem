using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.Repositories;
using System.Application.Data.Repositories.Orders;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Application.Validators.Addresses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Adresses
{
    public class AdressService : PrepareDefault
    {
        private readonly AdressRepository _addressRepository;
        private readonly OrderRepository _orderRepository;
        private readonly ILogger<AdressService> _logger;

        public AdressService(AdressRepository repository , ILogger<AdressService> logger, OrderRepository orderRepository)
        {
            this._addressRepository = repository;
            this._orderRepository = orderRepository;
            this._logger = logger;
        }

        public async Task<DefaultResponse> Create(AdressPostRequest _postRequest)
        {
            if (_postRequest.costumerId == Guid.Empty)
            { 
                _logger.LogError($"[AddressService][Create] There was an error with the request");
                return ErrorResponse(AddressErrors.Address_Post_400_CostumerId_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new AddressPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[AddressService][Create] Error while trying to create Address: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            var result = await _addressRepository.Create(new AdressEntity(_postRequest));
            if (result.Id == Guid.Empty)
                return ErrorResponse(AddressErrors.Address_Post_400_Create_Error.GetDescription());
            
            return SuccessResponse(result);
        }

        public async Task<DefaultResponse> Update(AdressPutRequest _putRequest)
        {
            if(_putRequest.Id == Guid.Empty)
            {
                _logger.LogError($"[AddressService][Update] ID was null or empty");
                return ErrorResponse(AddressErrors.Address_Put_400_AddressID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (_putRequest.costumerId == Guid.Empty)
            {
                _logger.LogError($"[AddressService][Update] Costumer ID was empty");
                return ErrorResponse(AddressErrors.Address_Put_400_CostumerId_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new AddressPutRequestValidator().Validate(_putRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[AddressService][Update] Error while trying to create Address: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            var result = await _addressRepository.Update(new AdressEntity(_putRequest));
            if (result.Id == Guid.Empty)
                return ErrorResponse(AddressErrors.Address_Put_400_Update_Error.GetDescription());

            return SuccessResponse(result);
        }

        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"[AddressService][Delete] ID was empty.");
                return ErrorResponse(AddressErrors.Address_Delete_400_AddressID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await _orderRepository.GetOrderByAddressId(id) != null)
            {
                _logger.LogError($"[AddressService][Delete] An order with this Address exists.");
                return ErrorResponse(AddressErrors.Address_Delete_400_ForeignKey_Error.GetDescription());
            }

            if (await _addressRepository.Get(id) == null)
            {
                _logger.LogError($"[AddressService][Delete] It was impossible to find an Address with this ID.");
                return ErrorResponse(AddressErrors.Address_Delete_400_AddressID_DoesNotExists.GetDescription());
            }

            if (!await _addressRepository.Delete(id))
            {
                _logger.LogError($"[AddressService][Delete] Database Error.");
                return ErrorResponse(AddressErrors.Address_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("Address was Sucessfully deleted.");
        }

        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"[AddressService][Get] ID was empty.");
                return ErrorResponse(AddressErrors.Address_Get_400_AddressID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await _addressRepository.Get(id);
            if (result == null)
            {
                _logger.LogError($"[AddressService][Get] It was impossible to find a costumer with this ID.");
                return ErrorResponse(AddressErrors.Address_Get_400_AddressID_DoesNotExists.GetDescription());
            }

            return SuccessResponse(result);
        }
    }
}
