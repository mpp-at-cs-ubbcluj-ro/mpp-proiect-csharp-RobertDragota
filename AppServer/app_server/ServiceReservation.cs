using System;
using System.Collections.Generic;
using AppDomain.Domain;
using AppPersistence.Repository;
using AppServices.Service;
using Lab3.Utility.Validation;

namespace AppServer.app_server;

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
        catch (ValidException e)
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
        catch (ValidException e)
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
        catch (ValidException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<Reservation> GetAll()
    {
        return _repository.GetAll();
    }

    public Reservation createReservation(Account account, string clientName, string phoneNumber, long tickets,
        Trip trip)
    {
        try
        {
            Reservation reservation = new Reservation(account,  phoneNumber, tickets, trip,clientName);
            _validator.validate(reservation);
           var a= _repository.Save(reservation);
            return reservation;
        }
        catch (ValidException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}