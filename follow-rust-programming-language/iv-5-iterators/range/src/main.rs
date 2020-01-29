fn main() {
    for x in 0..10 {
        println!("{}", x);
    }

    let mut range = 0..10;
    loop {
        match range.next() {
            Some(x) => {
                println!("{}", x);
            },
            None => { break }
        }
    }

    let _one_to_one_hundred = (1..101).collect::<Vec<_>>();

    let greater_than_forty_two = (0..100).find(|x| *x > 42);
    match greater_than_forty_two {
        Some(_) => println!("We got some numbers!"),
        None => println!("No numbers found"),
    }

    let sum = (1..4).fold(0, |sum, x| sum + x);
    println!("Sum is {}", sum);

    for i in (1..).step_by(5).take(5) {
        println!("{}", i);
    }

    let common_to_six =
        (1..1000)
            .filter(|&x| x % 2 == 0)
            .filter(|&x| x % 3 == 0)
            .filter(|&x| x % 5 == 0)
            .filter(|&x| x % 7 == 0)
            .take(5)
            .collect::<Vec<_>>();
    for i in common_to_six {
        println!("{}", i);
    }

    println!("Hello, world!");
}
