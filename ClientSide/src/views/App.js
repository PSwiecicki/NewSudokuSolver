import React from 'react';
import { Wrapper, StyledHeader } from './App.styles';
import Sudoku from 'components/Sudoku/Sudoku';

function App() {
  return (
    <Wrapper>
      <StyledHeader>
        <h1>Sudoku Solver</h1>
      </StyledHeader>
      <Sudoku />
    </Wrapper>
  );
}

export default App;
