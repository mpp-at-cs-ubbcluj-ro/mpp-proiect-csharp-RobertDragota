using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lab3.Domain;
using Lab3.Repository;
using Lab3.Utility.Validation;

namespace Lab3.Service;

public class ServiceReservation : IServiceReservation
{
    private IRepositoryReservation _repository;
    private Validator<Reservation> _validator;

    public ServiceReservation(IRepositoryReservation repository, Validator<Reservation> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public Reservation FindOne(long id)
    {
        return _repository.FindOne(id);
    }

    public Reservation Save(Reservation entity)
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

    public Reservation Delete(Reservation entity)
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

    public Reservation Update(Reservation entity)
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

    public List<Reservation> GetAll()
    {
        return _repository.GetAll();
    }
}