using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lab3.Domain;
using Lab3.Repository;
using Lab3.Utility.Validation;

namespace Lab3.Service;

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
        catch (ValidationException e)
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
        catch (ValidationException e)
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
        catch (ValidationException e)
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
}