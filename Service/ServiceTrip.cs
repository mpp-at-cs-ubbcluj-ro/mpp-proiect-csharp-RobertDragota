using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lab3.Domain;
using Lab3.Repository;
using Lab3.Utility.Validation;

namespace Lab3.Service;

public class ServiceTrip : IServiceTrip
{
    private readonly IRepositoryTrip _repository;
    private Validator<Trip> _validator;

    public ServiceTrip(IRepositoryTrip repository, Validator<Trip> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public Trip FindOne(long id)
    {
        return _repository.FindOne(id);
    }

    public Trip Save(Trip entity)
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

    public Trip Delete(Trip entity)
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

    public Trip Update(Trip entity)
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

    public List<Trip> GetAll()
    {
        return _repository.GetAll();
    }

    public Trip findByDestination(string destination)
    {
        
        return  _repository.findByDestination(destination);
    }

    public List<Trip> filterTrips(string destination, int startHour, int finishHour)
    {
        
        return _repository.filterTrips(destination, startHour, finishHour);
    }
}