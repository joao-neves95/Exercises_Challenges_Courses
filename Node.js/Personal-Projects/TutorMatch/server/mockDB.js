const Tutor = require( '../common/models/tutor' );
const Student = require( '../common/models/student' );
const CityName = require( '../common/enums/cityName' );

module.exports = {
  tutors: [
    new Tutor( "John Snow", CityName.London, [new Student()] ),
    new Tutor( "Jaime Lannister", CityName.Liverpool, [new Student(), new Student()] ),
    new Tutor( "Daenerys Targaryen", CityName.Manchester, [new Student(), new Student(), new Student()] ),
    new Tutor( "Sansa Stark", CityName.London, [new Student(), new Student()] ),
    new Tutor( "Theon Greyjoy", CityName.Manchester, [new Student()] ),
    new Tutor( "Arya Stark", CityName.London, [new Student()] ),
    new Tutor( "Khal Drogo", CityName.London, [new Student(), new Student(), new Student(), new Student(), new Student()] ),
    new Tutor( "Brienne of Tarth", CityName.Manchester, [new Student(), new Student(), new Student()] ),
    new Tutor( "The High Sparrow", CityName.London, [new Student(), new Student()] ),
    new Tutor( "Grey Worm", CityName.Liverpool, [new Student(), new Student(), new Student()] ),
    new Tutor( "Margaery Tyrell", CityName.Liverpool, [new Student(), new Student(), new Student(), new Student()] ),
    new Tutor( "Talisa Maegyr", CityName.Manchester, [new Student(), new Student()] ),
    new Tutor( "Joffrey Baratheon", CityName.London, [new Student(), new Student(), new Student()] ),
    new Tutor( "Robb Stark", CityName.Manchester, [new Student()] ),
    new Tutor( "Bronn", CityName.Liverpool, [new Student(), new Student(), new Student(), new Student()] ),
    new Tutor( "Loras Tyrell", CityName.London, [new Student(), new Student()] )
  ]
};
