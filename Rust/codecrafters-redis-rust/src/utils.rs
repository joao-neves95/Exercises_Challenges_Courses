pub struct LineEndings {}

impl LineEndings {
    pub const CRLF_STR: &str = "\r\n";

    pub const LF_STR: &str = "\n";
    pub const LF_CHAR: char = '\n';

    pub const CR_STR: &str = "\r";
    pub const CR_CHAR: char = '\r';
}

pub fn concat_u32(left: u32, right: u32) -> Option<u32> {
    let pow_result = 10u32.checked_pow(u32_count(right));

    if pow_result.is_none() {
        return None;
    }

    Some((left * pow_result.unwrap()) + right)
}

pub fn u32_count(value: u32) -> u32 {
    let mut counter = 1;
    let mut width_meter = 10;

    while width_meter <= value {
        width_meter *= 10;
        counter += 1;
    }

    counter
}

/// Appends to an array from the inclusive `start` and stops when it reaches the inclusive `end`.
///
/// This function does not offer overflow protection, so the caller has to account for that and choose an appropriate `end` smaller than `source`.
pub fn append_to_array<T>(target: &mut [T], source: &[T], start_idx: usize, end_idx: usize)
where
    T: Copy,
{
    let mut i_source: usize = 0;

    for i_target in start_idx..(end_idx + 1) {
        if i_source == end_idx + 1 {
            break;
        }

        target[i_target] = source[i_source];
        i_source += 1;
    }
}

/// Appends to an array from the inclusive `start` and stops with the use of the `until` conditional closure.
///
/// This function does not offer overflow protection, so the caller has to account for that.
pub fn append_to_array_until<T, F>(target: &mut [T], source: &[T], start: usize, until: F)
where
    T: Copy,
    F: Fn(T, usize, usize) -> bool,
{
    let mut i_target: usize = start;
    let mut i_source: usize = 0;

    loop {
        let item = source[i_source];

        if until(item, i_target, i_source) {
            break;
        }

        target[i_target] = item;
        i_target += 1;
        i_source += 1;
    }
}

#[cfg(test)]
mod tests {
    use crate::utils::append_to_array;

    use super::append_to_array_until;

    #[test]
    fn append_to_array_passes() {
        let mut target = [0; 10];
        let source = [1; 5];

        append_to_array(&mut target, &source, 1, 4);

        assert_eq!(target[0], 0);
        assert_eq!(target[1], 1);
        assert_eq!(target[2], 1);
        assert_eq!(target[3], 1);
        assert_eq!(target[4], 1);
        assert_eq!(target[5], 0);
        assert_eq!(target[6], 0);
    }

    #[test]
    fn append_to_array_until_passes() {
        let mut target = [0; 10];

        let mut source = [1; 10];
        for i in 7..10 {
            source[i] = 0;
        }
        let source = source;

        append_to_array_until(&mut target, &source, 2, |item, _, _| item == 0);

        assert_eq!(target[0], 0);
        assert_eq!(target[1], 0);
        assert_eq!(target[2], 1);
        assert_eq!(target[3], 1);
        assert_eq!(target[6], 1);
        assert_eq!(target[7], 1);
        assert_eq!(target[8], 1);
        assert_eq!(target[9], 0);
    }
}
