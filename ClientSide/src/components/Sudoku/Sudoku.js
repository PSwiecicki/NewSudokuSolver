import React, { useState } from 'react';
import { SudokuWrapper, StyledInput, Wrapper } from './Sudoku.styles';

const dataToString = (data) =>
  data.reduce(
    (acc, row) =>
      acc +
      row.reduce((rowAcc, value) => rowAcc + (value === '' ? '0' : value), ''),
    ''
  );

const initialData = [
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
  ['', '', '', '', '', '', '', '', ''],
];

const Sudoku = () => {
  const [data, setData] = useState(initialData);

  const handleClick = () => {
    fetch('https://localhost:44387/sudoku')
      .then((response) => response.json())
      .then((responseData) => setData(responseData));
  };

  const handleOnKeyDown = (e) => {
    console.log(e);
    const regEx = /\d/g;
    const [row, column] = e.target.id.match(regEx);
    const newData = data.map((row) => row.slice());
    if (!regEx.test(e.nativeEvent.data)) {
      e.target.value = '';
    }
    newData[row][column] = e.nativeEvent.data;
    fetch(`https://localhost:44387/sudoku/${dataToString(newData)}`)
      .then((response) => response.json())
      .then((val) => {
        console.log(val);
        if (val) setData(newData);
      });
  };

  return (
    <Wrapper>
      <SudokuWrapper>
        {data.map((row, rowIndex) =>
          row.map((value, columnIndex) => (
            <StyledInput
              key={`${rowIndex}${columnIndex}`}
              id={`input${rowIndex}${columnIndex}`}
              row={rowIndex}
              column={columnIndex}
              onChange={handleOnKeyDown}
              value={data[rowIndex][columnIndex]}
              maxLength="1"
            />
          ))
        )}
      </SudokuWrapper>
      <div>
        <button onClick={handleClick}>Solve!</button>
      </div>
    </Wrapper>
  );
};

export default Sudoku;
