#include <time.h>

#include "date.h"

#define AVG_DAYS_IN_MONTH 30.4167
#define AVG_DAYS_IN_YEAR 365.24225

Date newDate( int day, int month, int year ) {
    Date date;
    date.day = day;
    date.month = month;
    date.year = year;

    return date;
}

// time.h - https://en.cppreference.com/w/cpp/chrono/c
// struct tm - https://en.cppreference.com/w/cpp/chrono/c/tm

struct tm* getCurrentDate() {
    time_t nowTime;
    // Get the current system's calendar time in seconds.
    time( &nowTime );
    // Convert to a tm struct.
    return localtime( &nowTime );
}

int getTotalDaysFromBirth( const Date date ) {
    struct tm* now = getCurrentDate();
    // Because it starts at 0 (0 days from January).
    ++now->tm_mon;
    // Because it return the years after the year 1900.
    now->tm_year = now->tm_year + 1900;
    
    if (date.day > now->tm_mday) {
        now->tm_mday += 30;
        --now->tm_mon;
    }
    
    if (date.month > now->tm_mon) {
        now->tm_mon += 12;
        --now->tm_year;
    }
                    // Days difference.                    // Days in the months difference.                    // Days in the years difference.
    int result = (now->tm_mday - date.day) + ( ( now->tm_mon - date.month ) * AVG_DAYS_IN_MONTH ) + ( (now->tm_year - date.year) * AVG_DAYS_IN_YEAR );
    return result;
}

int addDateNums( const Date date ) {
    return date.day + date.month + date.year;
}

