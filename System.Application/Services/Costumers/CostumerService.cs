using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.MySql;
using System.Application.Data.Repositories;
using System.Application.Data.Repositories.Costumers;
using System.Application.Data.Repositories.Orders;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Application.Validators.Costumers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Costumers
{

    public class CostumerService : PrepareDefault
    {
        private readonly CostumerRepository _costumerRepository;
        private readonly ILogger<CostumerService> _logger;
        private readonly OrderRepository _orderRepository;
        private readonly AdressRepository _addressRepository;

        public CostumerService(CostumerRepository repository, ILogger<CostumerService> logger, OrderRepository orderRepository, AdressRepository addressRepository)
        {
            this._costumerRepository = repository;
            this._logger = logger;
            this._orderRepository = orderRepository;
            this._addressRepository = addressRepository;
        }

        public async Task<DefaultResponse> Create(CostumerPostRequest _postRequest)
        {
            var validator = new CostumerPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[CostumerService][Create] Error while trying to create costumer: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await _costumerRepository.GetByDocument(_postRequest.document) != null)
            {
                _logger.LogError($"[CostumerService][Create] A costumer with this document already exists.");
                return ErrorResponse(CostumerErrors.Costumer_Post_400_Document_Cannot_Be_Duplicated.GetDescription());
            }

            var result = await _costumerRepository.Create(new CostumerEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                _logger.LogError($"[CostumerService][Create] Database Error.");
                return ErrorResponse(CostumerErrors.Costumer_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }

        public async Task<DefaultResponse> Update(CostumerPutRequest _putRequest)
        {
            if (_putRequest.id == Guid.Empty)
            {
                _logger.LogError($"[CostumerService][Update] ID was null or empty.");
                return ErrorResponse(CostumerErrors.Costumer_Put_400_CostumerID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new CostumerPutRequestValidator().Validate(_putRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[CostumerService][Update] Error while trying to update costumer: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await _costumerRepository.Get(_putRequest.id) == null)
            {
                _logger.LogError($"[CostumerService][Update] It was impossible to find a costumer with this ID.");
                return ErrorResponse(CostumerErrors.Costumer_Put_400_CostumerID_DoesNotExists.GetDescription());
            }

            var costumer = await _costumerRepository.GetByDocument(_putRequest.document);
            if (costumer != null && costumer.Id != _putRequest.id)
            {
                _logger.LogError($"[CostumerService][Update] A costumer with this document already exists.");
                return ErrorResponse(CostumerErrors.Costumer_Put_400_CostumerDocument_Cannot_Be_Duplicated.GetDescription());
            }

            var result = await _costumerRepository.Update(new CostumerEntity(_putRequest));
            if (result.Id == Guid.Empty)
            {
                _logger.LogError($"[CostumerService][Update] Database Error.");
                return ErrorResponse(CostumerErrors.Costumer_Put_400_Update_Error.GetDescription());
            }

            return SuccessResponse(result);
        }

        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"[CostumerService][Delete] ID was empty.");
                return ErrorResponse(CostumerErrors.Costumer_Delete_400_CostumerID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await _orderRepository.GetOrderByCostumerId(id) != null)
            {
                _logger.LogError($"[CostumerService][Delete] An active order exists for this costumer.");
                return ErrorResponse("An active order exists for this costumer.");
            }

            if (await _addressRepository.GetAddressByCostumerId(id) != null)
            {
                _logger.LogError($"[CostumerService][Delete] An active address exists for this costumer.");
                return ErrorResponse("An active address exists for this costumer.");
            }

            if (await _costumerRepository.Get(id) == null)
            {
                _logger.LogError($"[CostumerService][Delete] It was impossible to find a costumer with this ID.");
                return ErrorResponse(CostumerErrors.Costumer_Delete_400_CostumerID_DoesNotExists.GetDescription());
            }

            if (!await _costumerRepository.Delete(id))
            {
                _logger.LogError($"[CostumerService][Delete] Database Error.");
                return ErrorResponse(CostumerErrors.Costumer_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("Costumer was sucessfully deleted.");
        }

        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"[CostumerService][Get] ID was empty.");
                return ErrorResponse(CostumerErrors.Costumer_Get_400_CostumerID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await _costumerRepository.Get(id);
            if (result == null)
            {
                _logger.LogError($"[CostumerService][Get] It was impossible to find a costumer with this ID.");
                return ErrorResponse(CostumerErrors.Costumer_Get_400_CostumerID_DoesNotExists.GetDescription());
            }

            return SuccessResponse(result);
        }
    }
}
