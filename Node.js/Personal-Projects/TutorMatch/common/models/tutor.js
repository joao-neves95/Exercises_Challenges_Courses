let __Student;

try {
  __Student = require( './student' );

} catch ( e ) {
  // continue;
}

class Tutor {

  constructor( name, city, students ) {
    this.name = name;
    this.city = city;
    this.students = students;
  }

}

try {
  // Node.js
  module.exports = Tutor;

} catch ( e ) {
  // continue;
}
