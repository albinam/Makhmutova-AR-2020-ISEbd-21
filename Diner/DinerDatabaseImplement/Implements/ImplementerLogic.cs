﻿using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
using DinerDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerDatabaseImplement.Implements
{
    public class ImplementerLogic : IImplementerLogic
    {
        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec =>
                        rec.ImplementerFIO == model.ImplementerFIO && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Такой исполнитель уже существует");
                }
                if (model.Id.HasValue)
                {
                    element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Implementer();
                    context.Implementers.Add(element);
                }
                element.ImplementerFIO = model.ImplementerFIO;
                element.WorkingTime = model.WorkingTime;
                element.PauseTime = model.PauseTime;
                context.SaveChanges();
            }
        }
        public void Delete(ImplementerBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Implementers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            using (var context = new DinerDatabase())
            {
                return context.Implementers
                .Where(
                    rec => model == null
                    || rec.Id == model.Id
                )
                .Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime
                })
                .ToList();
            }
        }
    }
}
