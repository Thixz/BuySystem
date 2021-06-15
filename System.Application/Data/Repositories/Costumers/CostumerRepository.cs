using Dapper;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.Costumers
{
    public class CostumerRepository
    {
        private readonly MySqlContext _sqlContext;
        public CostumerRepository(MySqlContext _context)
        {
            this._sqlContext = _context;
        }

        public virtual async Task<CostumerEntity> Create(CostumerEntity _costumerEntity)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = @"insert into costumers (id, costumerName, document, phoneNumber, email, 
                                        birthdate, gender)
                                        values (@Pid, @PcostumerName, @Pdocument, @PphoneNumber, @Pemail, 
                                        @Pbirthdate, @Pgender)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _costumerEntity.Id,
                        PcostumerName = _costumerEntity.costumerName,
                        Pdocument = _costumerEntity.document,
                        PphoneNumber = _costumerEntity.phoneNumber,
                        Pemail = _costumerEntity.email,
                        Pbirthdate = _costumerEntity.birthdate,
                        Pgender = _costumerEntity.gender,
                    });

                    Console.WriteLine("[CostumerRepository][Create] Costumer was successfully created!");
                    return _costumerEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[CostumerRepository][Create] Error while trying to create costumer. " + ex);
                    return new CostumerEntity();
                }
            }
        }

        public virtual async Task<CostumerEntity> Update(CostumerEntity _costumerEntity)
        {
            try
            {
                using (var cnx = _sqlContext.Connect())
                {
                    string sqlQuery = @"update costumers set costumerName = @PcostumerName,
                                                         document = @Pdocument,
                                                         phoneNumber = @PphoneNumber, 
                                                         email = @Pemail,   
                                                         birthdate = @Pbirthdate, 
                                                         gender = @Pgender
                                                         where id = @Pid";
                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _costumerEntity.Id,
                        PcostumerName = _costumerEntity.costumerName,
                        Pdocument = _costumerEntity.document,
                        PphoneNumber = _costumerEntity.phoneNumber,
                        Pemail = _costumerEntity.email,
                        Pbirthdate = _costumerEntity.birthdate,
                        Pgender = _costumerEntity.gender,
                    }); 

                    Console.WriteLine("[CostumerRepository][Update] Costumer was successfully updated!");
                    return _costumerEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CostumerRepository][Update] Error while trying to update costumer. " + ex);
                return new CostumerEntity();
            }
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $"delete from costumers where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    Console.WriteLine("[CostumerRepository][Delete] Costumer was successfully deleted!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[CostumerRepository][Delete] Error while trying to delete costumer. " + ex);
                    return false;
                }
            }
        }

        public virtual async Task<CostumerEntity> Get(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $@"select id,costumerName,document,phoneNumber,email,
                                                birthdate,gender,creationDate,updatedDate from costumers 
                                                where id = '{id}'";
                    var query = await cnx.QueryFirstOrDefaultAsync<CostumerEntity>(sqlQuery);
                    Console.WriteLine("[CostumerRepository][Get] Costumer was successfully consulted!");
                    return query;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[CostumerRepository][Get] Error while trying to consult costumer. " + ex);
                    return new CostumerEntity();
                }
            }
        }

        public virtual async Task<CostumerEntity> GetByDocument(string document)
        {
            using (var cnx = _sqlContext.Connect())
            {
                        string sqlQuery = $@"select id,costumerName,document,phoneNumber,email,
                                                birthdate,gender,creationDate,updatedDate from costumers 
                                                where document = '{document}'";
                        var query = await cnx.QueryFirstOrDefaultAsync<CostumerEntity>(sqlQuery);
                        return query;
            }
        }
    }
}
