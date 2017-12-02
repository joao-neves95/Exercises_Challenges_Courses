const date = new Date().toISOString().substr(0, 10);
const time = new Date().toISOString().substr(11, 5)
const dateTime = date + ' ' + time

console.log(dateTime);  // 2017-12-02 18:17
