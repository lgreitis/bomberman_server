using GameServices.Models.BombModels;
using GameServices.Models.CommonModels;

var bomb = new RegularBomb();
bomb.PlacedPosition = new Position(1, 1);
var bombpos = bomb.PlacedPosition;

var deep = (RegularBomb)bomb.DeepCopy();
var deeppos = deep.PlacedPosition;

var shallow = (RegularBomb)bomb.ShallowCopy();
var shallowpos = shallow.PlacedPosition;

Console.ReadKey();