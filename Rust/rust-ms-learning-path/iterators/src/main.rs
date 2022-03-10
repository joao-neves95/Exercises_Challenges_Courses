struct Groups<T> {
    inner: Vec<T>,
    i: usize,
}

impl<T> Groups<T> {
    fn new(inner: Vec<T>) -> Self {
        Groups { inner, i: 0 }
    }
}

impl<T: PartialEq + Clone> Iterator for Groups<T> {
    type Item = Vec<T>;

    // TODO: Write the rest of this implementation.
    fn next(&mut self) -> Option<Self::Item> {
        let total_len = self.inner.len();

        if self.i == total_len {
            return None;
        }

        let mut response = Vec::<T>::new();

        for idx in self.i..total_len {
            let curr_val = &self.inner[idx];
            response.push((*curr_val).clone());

            self.i += 1;

            if self.i == total_len || self.inner[idx + 1] != *curr_val {
                break;
            }
        }

        Some(response)
    }
}

fn main() {
    let data = vec![4, 1, 1, 2, 1, 3, 3, -2, -2, -2, 5, 5];
    // groups:     |->|---->|->|->|--->|----------->|--->|
    assert_eq!(
        Groups::new(data).into_iter().collect::<Vec<Vec<_>>>(),
        vec![
            vec![4],
            vec![1, 1],
            vec![2],
            vec![1],
            vec![3, 3],
            vec![-2, -2, -2],
            vec![5, 5],
        ]
    );

    let data2 = vec![1, 2, 2, 1, 1, 2, 2, 3, 4, 4, 3];
    // groups:      |->|---->|---->|----|->|----->|->|
    assert_eq!(
        Groups::new(data2).into_iter().collect::<Vec<Vec<_>>>(),
        vec![
            vec![1],
            vec![2, 2],
            vec![1, 1],
            vec![2, 2],
            vec![3],
            vec![4, 4],
            vec![3],
        ]
    );

    println!("Done.");
}
