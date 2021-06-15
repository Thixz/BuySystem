using Dapper;
using System;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories
{
    public class AdressRepository
    {
        private readonly MySqlContext _sqlContext;
        public AdressRepository(MySqlContext _context)
        {
            this._sqlContext = _context;
        }


        public virtual async Task<AdressEntity> Create(AdressEntity _adressEntity)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = @"insert into addresses (id, costumerId, address, addressNumber, neighborhood, postalCode, country)
                                        values (@Pid, @PcostumerId, @Paddress, @PaddressNumber, @Pneighborhood, @PpostalCode, @Pcountry)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _adressEntity.Id,
                        PcostumerId = _adressEntity.costumerId,
                        Paddress = _adressEntity.address,
                        PaddressNumber = _adressEntity.addressNumber,
                        Pnumber = _adressEntity.addressNumber,
                        Pneighborhood = _adressEntity.neighborhood,
                        PpostalCode = _adressEntity.postalCode,
                        Pcountry = _adressEntity.country,
                    });

                    Console.WriteLine("[AddressRepository][Create] Address was successfully created!");
                    return _adressEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[AddressRepository][Create] Error while trying to create address. " + ex);
                    return new AdressEntity();
                }
            }
        }
        public virtual async Task<AdressEntity> Update(AdressEntity _adressEntity)
        {
            try
            {
                using (var cnx = _sqlContext.Connect())
                {
                    string sqlQuery = @"update addresses set costumerId = @PcostumerId,
                                                         address = @Paddress,
                                                         addressNumber = @PaddressNumber, 
                                                         neighborhood = @Pneighborhood, 
                                                         postalCode = @PpostalCode, 
                                                         country = @Pcountry 
                                                         where id = @Pid";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _adressEntity.Id,
                        PcostumerId = _adressEntity.costumerId,
                        Paddress = _adressEntity.address,
                        PaddressNumber = _adressEntity.addressNumber,
                        Pneighborhood = _adressEntity.neighborhood,
                        PpostalCode = _adressEntity.postalCode,
                        Pcountry = _adressEntity.country,
                    });

                    Console.WriteLine("[AddressRepository][Update] Address was successfully updated!");
                    return _adressEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AddressRepository][Update] Error while trying to update address. " + ex);
                return new AdressEntity();
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $"delete from addresses where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    Console.WriteLine("[AddressRepository][Delete] Address was successfully deleted!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[AddressRepository][Delete] Error while trying to delete address. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<AdressEntity> Get(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $@"select id,costumerId,address,addressNumber,postalCode,neighborhood,country,
                                                creationDate,updatedDate from addresses 
                                                where id = '{id}'";
                    var query = await cnx.QueryFirstOrDefaultAsync<AdressEntity>(sqlQuery);
                    Console.WriteLine("[AddressRepository][Get] Address was successfully consulted!");
                    return query;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[AddressRepository][Get] Error while trying to consult address. " + ex);
                    return new AdressEntity();
                }
            }
        }
        public virtual async Task<AdressEntity> GetAddressByCostumerId(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id,costumerId,address,addressNumber,postalCode,neighborhood,country,
                                                creationDate,updatedDate from addresses 
                                                where costumerId = '{id}'";
                var query = await cnx.QueryFirstOrDefaultAsync<AdressEntity>(sqlQuery);
                return query;
            }
        }
    }
}
