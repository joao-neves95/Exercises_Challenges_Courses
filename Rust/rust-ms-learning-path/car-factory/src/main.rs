use std::collections::HashMap;

#[derive(Clone, Debug)]
struct Car {
    color: String,
    transmission: Transmission,
    convertible: bool,
    age: (Age, u32),
}

// impl Car {
//     fn add_mileage(&mut self, mileage: u32) {
//         self.mileage += mileage;
//     }
// }

#[derive(Clone, PartialEq, Debug)]
enum Age {
    New,
    Used,
}

#[derive(Clone, PartialEq, Debug)]
enum Transmission {
    Manual,
    SemiAuto,
    Automatic,
}

fn car_quality(miles: u32) -> (Age, u32) {
    (if miles == 0 { Age::New } else { Age::Used }, miles)
}

fn car_factory(order_id: u32, miles: u32, print_details: Option<bool>) -> Car {
    let colors = [
        "Black".to_string(),
        "White".to_string(),
        "Grey".to_string(),
        "Silver".to_string(),
        "Gold".to_string(),
    ];

    let mut color_idx = order_id as usize;
    while color_idx > colors.len() - 1 {
        color_idx = color_idx - (colors.len() - 1);
    }

    let transmission: Transmission;
    let mut convertible = false;

    if order_id % 2 == 0 {
        transmission = Transmission::Manual;
    } else if order_id % 3 == 0 {
        transmission = Transmission::Automatic;
        convertible = true;
    } else {
        transmission = Transmission::SemiAuto;
    }

    let car = Car {
        color: colors[color_idx].to_string(),
        transmission: transmission,
        convertible: convertible,
        age: car_quality(miles),
    };

    if print_details.unwrap_or(false) {
        print_car_order_details(order_id, &car);
    }

    return car;
}

fn print_car_order_details(order_id: u32, car: &Car) {
    println!("Car order: {}: {:?}", order_id, car);
    print_convertible_car_details(car);
}

fn print_convertible_car_details(car: &Car) {
    if !(car.convertible) {
        return;
    }

    println!(
        "This is a used non-convertible car! Here are some details: {:?}, color {}, hard top, with {} miles",
        car.transmission, car.color, car.age.1
    );
}

fn main() {
    println!("Hello, world!");

    println!("Let's create some cars.");

    let cars_to_create_num = 10;

    let mut orders: HashMap<u32, Car> = HashMap::new();

    for i in 0..cars_to_create_num {
        let is_even: bool = i % 2 == 0;

        orders.insert(
            i,
            car_factory(i, if is_even { 0 } else { i * 1000 }, Some(false)),
        );

        print_car_order_details(i, orders.get(&i).unwrap_or(&car_factory(i, 0, None)));
    }

    // println!("Let's increase mileage.");
    // new_car.add_mileage(420);
    // println!("{:#?}", new_car);
}
