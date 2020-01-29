fn foo(x: i32) -> i32 { x }

fn main() {

    let a = [0; 10];
    let middle = &a[1..4];
    let complete = &a[..];

    let x: fn(i32) -> i32 = foo;

    println!("Hello, world!");
}
