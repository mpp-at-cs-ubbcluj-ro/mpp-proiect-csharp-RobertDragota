using System;
using System.Collections.Generic;
using AppDomain.Domain;
using AppPersistence.Repository;
using AppServices.Service;
using Lab3.Utility.Validation;

namespace AppServer.app_server;

public class ServiceAccount: IServiceAccount
{
    private readonly IRepositoryAccount _repository;
    private readonly Validator<Account> _validator;

    public ServiceAccount(IRepositoryAccount repository, Validator<Account> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public Account FindOne(long id)
    {
       
        return _repository.FindOne(id);
    }

    public Account Save(Account entity)
    {
        try
        {
            _validator.validate(entity);
            return _repository.Save(entity);
        }
        catch (ValidException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        
    }

    public Account Delete(Account entity)
    {
        try
        {
            _validator.validate(entity);
            return _repository.Delete(entity);
        }
        catch (ValidException e)
        {
            Console.WriteLine(e.Message);
            return null;
        
        }
       
    }

    public Account Update(Account entity)
    {
        try
        {
            _validator.validate(entity);
            return _repository.Update(entity);
        }
        catch (ValidException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<Account> GetAll()
    {
        return _repository.GetAll();
    }

    public Account findByUsername(string username)
    {
        
        return _repository.findByUsername(username);
    }

    public Account findByUsernameAndPassword(string username, string password)
    {
        return _repository.findByUsernameAndPassword(username, password);
    }

    public Account createAccount(string username, string password)
    {
        try
        {
            Account account = new Account(username, password);
            _validator.validate(account);
            return account;
        }
        catch (ValidException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}