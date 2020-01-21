
typedef struct Date {
    int day;
    int month;
    int year;

} Date;

Date newDate( int day, int month, int year );

struct tm* getCurrentDate();

Date getTotalSecondsFromBirth( const Date date );

int addDateNums( const Date date );

