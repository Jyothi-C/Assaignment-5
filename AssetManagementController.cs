using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AssetManagementSystem.Models;
using AssetManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
namespace AssetManagementSystem.Controllers
{
    public class AssetManagementController : Controller
    {
        private readonly AssetManagementSystemContext _Context;
        public AssetManagementController(AssetManagementSystemContext context)
        {
            _Context = context;
        }
        static List<AssetModel> assetList = new List<AssetModel>();
        [HttpGet]
        public ActionResult SearchAsset(string search)
        {
            assetList.AddRange(_Context.BookAssets.
            Where(b => string.IsNullOrEmpty(search) ? true : (b.Author.Contains(search) || b.Name.Contains(search) || b.DateOfPublish.Contains(search) || Convert.ToString(b.Id).Equals(search) || b.Genre.Contains(search))).Select(x => new AssetModel
            {
                AssetType = TypeOfAsset.Book,
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                DateOfPublish = x.DateOfPublish,
                Genre = x.Genre
            }));
            assetList.AddRange(_Context.SoftwareAssets.
            Where(b => string.IsNullOrEmpty(search) ? true : (b.OsPlatform.Contains(search) || b.Name.Contains(search) || b.DateOfPublish.Contains(search) || Convert.ToString(b.Id).Equals(search) || b.SoftwareCompany.Contains(search) || b.Type.Contains(search))).Select(x => new AssetModel
            {
                AssetType = TypeOfAsset.Software,
                Id = x.Id,
                Name = x.Name,
                OsPlatform = x.OsPlatform,
                DateOfPublish = x.DateOfPublish,
                Type = x.Type,
                SoftwareCompany = x.SoftwareCompany
            }));
            assetList.AddRange(_Context.HardwareAssets.
            Where(b => string.IsNullOrEmpty(search) ? true : (b.Name.Contains(search) || Convert.ToString(b.Id).Equals(search) || b.DateOfPublish.Contains(search) || b.HardwareCompany.Contains(search) || b.SupportedDevice.Contains(search))).Select(x => new AssetModel
            {
                AssetType = TypeOfAsset.Hardware,
                Id = x.Id,
                Name = x.Name,
                HardwareCompany = x.HardwareCompany,
                DateOfPublish = x.DateOfPublish,
                SupportedDevice = x.SupportedDevice
            }));
            return View(assetList);
        }
        [HttpGet]
        public ActionResult Details(int assetType, int id)
        {
            AssetModel assetModel = new AssetModel();
            switch ((int)assetType)
            {
                case 1:
                    _Context.BookAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Author = x.Author,
                        DateOfPublish = x.DateOfPublish,
                        Genre = x.Genre
                    });
                    break;
                case 2:
                    _Context.SoftwareAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        AssetType = TypeOfAsset.Software,
                        Id = x.Id,
                        Name = x.Name,
                        OsPlatform = x.OsPlatform,
                        DateOfPublish = x.DateOfPublish,
                        Type = x.Type,
                        SoftwareCompany = x.SoftwareCompany
                    });
                    break;
                case 3:
                    _Context.HardwareAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        AssetType = TypeOfAsset.Hardware,
                        Id = x.Id,
                        Name = x.Name,
                        HardwareCompany = x.HardwareCompany,
                        DateOfPublish = x.DateOfPublish,
                        SupportedDevice = x.SupportedDevice
                    });
                    break;
            }
            return View(assetModel);
        }
        [HttpGet]
        public ActionResult AddAsset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAsset(AssetModel assetCreate)
        {
            if (ModelState.IsValid)
            {
                switch ((int)assetCreate.AssetType)
                {
                    case 1:
                        assetList.Add(assetCreate);
                        BookAsset book = new BookAsset();
                        book.Id = assetCreate.Id;
                        book.Name = assetCreate.Name;
                        book.DateOfPublish = assetCreate.DateOfPublish;
                        book.Author = assetCreate.Author;
                        book.Genre = assetCreate.Genre;
                        _Context.BookAssets.Add(book);
                        break;
                    case 2:
                        assetList.Add(assetCreate);
                        SoftwareAsset software = new SoftwareAsset();
                        software.Id = assetCreate.Id;
                        software.Name = assetCreate.Name;
                        software.DateOfPublish = assetCreate.DateOfPublish;
                        software.OsPlatform = assetCreate.OsPlatform;
                        software.Type = assetCreate.Type;
                        software.SoftwareCompany = assetCreate.SoftwareCompany;
                        _Context.SoftwareAssets.Add(software);
                        break;
                    case 3:
                        assetList.Add(assetCreate);
                        HardwareAsset hardware = new HardwareAsset();
                        hardware.Id = assetCreate.Id;
                        hardware.Name = assetCreate.Name;
                        hardware.DateOfPublish = assetCreate.DateOfPublish;
                        hardware.HardwareCompany = assetCreate.HardwareCompany;
                        hardware.SupportedDevice = assetCreate.SupportedDevice;
                        _Context.HardwareAssets.Add(hardware);
                        break;
                }
                _Context.SaveChanges();
                string message = "Added the Asset successfully";
                ViewBag.Message = message;
                return View();
            }
            return View(assetCreate);
        }
        [HttpGet]
        public ActionResult UpdateAsset(int assetType, int id)
        {
            AssetModel assetModel = new AssetModel();
            switch ((int)assetType)
            {
                case 1:
                    _Context.BookAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Author = x.Author,
                        DateOfPublish = x.DateOfPublish,
                        Genre = x.Genre
                    });
                    break;
                case 2:
                    _Context.SoftwareAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        AssetType = TypeOfAsset.Software,
                        Id = x.Id,
                        Name = x.Name,
                        OsPlatform = x.OsPlatform,
                        DateOfPublish = x.DateOfPublish,
                        Type = x.Type,
                        SoftwareCompany = x.SoftwareCompany
                    });
                    break;
                case 3:
                    _Context.HardwareAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        AssetType = TypeOfAsset.Hardware,
                        Id = x.Id,
                        Name = x.Name,
                        HardwareCompany = x.HardwareCompany,
                        DateOfPublish = x.DateOfPublish,
                        SupportedDevice = x.SupportedDevice
                    });
                    break;
            }
            return View(assetModel);
        }
        [HttpPost]
        public ActionResult UpdateAsset(AssetModel assetEdit)
        {
            List<AssetModel> assetList = new List<AssetModel>();
            if (ModelState.IsValid)
            {
                switch ((int)assetEdit.AssetType)
                {
                    case 1:
                        assetList.Add(assetEdit);
                        BookAsset book = new BookAsset();
                        book.Id = assetEdit.Id;
                        book.Name = assetEdit.Name;
                        book.DateOfPublish = assetEdit.DateOfPublish;
                        book.Author = assetEdit.Author;
                        book.Genre = assetEdit.Genre;
                        var asset = _Context.BookAssets.Attach(book);
                        asset.State = EntityState.Modified;
                        break;

                    case 2:
                        SoftwareAsset software = new SoftwareAsset();
                        software.Id = assetEdit.Id;
                        software.Name = assetEdit.Name;
                        software.DateOfPublish = assetEdit.DateOfPublish;
                        software.OsPlatform = assetEdit.OsPlatform;
                        software.Type = assetEdit.Type;
                        var asset1 = _Context.SoftwareAssets.Attach(software);
                        asset1.State = EntityState.Modified;
                        break;

                    case 3:
                        HardwareAsset hardware = new HardwareAsset();
                        hardware.Id = assetEdit.Id;
                        hardware.Name = assetEdit.Name;
                        hardware.DateOfPublish = assetEdit.DateOfPublish;
                        hardware.HardwareCompany = assetEdit.HardwareCompany;
                        hardware.SupportedDevice = assetEdit.SupportedDevice;
                        var asset2 = _Context.HardwareAssets.Attach(hardware);
                        asset2.State = EntityState.Modified;
                        break;
                }
                _Context.SaveChanges();
                string message = "Updated the Asset successfully";
                ViewBag.Message = message;
                return View(assetEdit);
            }
            else
            {
                string message = "NO data Modified";
                ViewBag.Message = message;
                return View(ViewBag.Message);
            }
        }
        [HttpGet]
        public ActionResult DeleteAsset(int assetType, int id)
        {
            AssetModel assetModel = new AssetModel();
            switch ((int)assetType)
            {
                case 1:
                    _Context.BookAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Author = x.Author,
                        DateOfPublish = x.DateOfPublish,
                        Genre = x.Genre
                    });
                    break;
                case 2:
                    _Context.SoftwareAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        AssetType = TypeOfAsset.Software,
                        Id = x.Id,
                        Name = x.Name,
                        OsPlatform = x.OsPlatform,
                        DateOfPublish = x.DateOfPublish,
                        Type = x.Type,
                        SoftwareCompany = x.SoftwareCompany
                    });
                    break;
                case 3:
                    _Context.HardwareAssets.Where(x => x.Id == id).Select(x => new AssetModel
                    {
                        AssetType = TypeOfAsset.Hardware,
                        Id = x.Id,
                        Name = x.Name,
                        HardwareCompany = x.HardwareCompany,
                        DateOfPublish = x.DateOfPublish,
                        SupportedDevice = x.SupportedDevice
                    });
                    break;
            }
            return View(assetModel);
        }
        [HttpPost]
        public ActionResult DeleteAsset(AssetModel assetDelete)
        {
            List<AssetModel> assetList = new List<AssetModel>();
            switch ((int)assetDelete.AssetType)
            {
                case 1:
                    BookAsset book = new BookAsset();
                    book.Id = assetDelete.Id;
                    book.Name = assetDelete.Name;
                    book.DateOfPublish = assetDelete.DateOfPublish;
                    book.Author = assetDelete.Author;
                    book.Genre = assetDelete.Genre;
                    var asset = _Context.BookAssets.FirstOrDefault(b => b.Id == assetDelete.Id);
                    _Context.BookAssets.Remove(asset);
                    break;
                case 2:
                    SoftwareAsset software = new SoftwareAsset();
                    software.Id = assetDelete.Id;
                    software.Name = assetDelete.Name;
                    software.DateOfPublish = assetDelete.DateOfPublish;
                    software.OsPlatform = assetDelete.OsPlatform;
                    software.Type = assetDelete.Type;
                    var asset1 = _Context.BookAssets.FirstOrDefault(b => b.Id == assetDelete.Id);
                    _Context.BookAssets.Remove(asset1);
                    break;
                case 3:
                    HardwareAsset hardware = new HardwareAsset();
                    hardware.Id = assetDelete.Id;
                    hardware.Name = assetDelete.Name;
                    hardware.DateOfPublish = assetDelete.DateOfPublish;
                    hardware.HardwareCompany = assetDelete.HardwareCompany;
                    hardware.SupportedDevice = assetDelete.SupportedDevice;
                    var asset2 = _Context.BookAssets.FirstOrDefault(b => b.Id == assetDelete.Id);
                    _Context.BookAssets.Remove(asset2);
                    break;
            }
            _Context.SaveChanges();
            return View(assetDelete);
        }
    }
}