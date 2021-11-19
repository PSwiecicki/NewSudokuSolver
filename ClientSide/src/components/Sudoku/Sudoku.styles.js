import styled from 'styled-components';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

export const SudokuWrapper = styled.div`
  display: grid;
  grid-template-columns: repeat(9, 60px);
  grid-template-rows: repeat(9, 60px);
`;

export const StyledInput = styled.input`
  text-align: center;
  font-size: 2rem;
  border-radius: 0;
  border-style: solid;
  border-color: #ff8519;
  border-top-width: ${({ row }) => (row % 3 === 0 ? '4px' : '1px')};
  border-bottom-width: ${({ row }) => (row === 8 ? '4px' : '1px')};
  border-left-width: ${({ column }) => (column % 3 === 0 ? '4px' : '1px')};
  border-right-width: ${({ column }) => (column === 8 ? '4px' : '1px')};
  background-color: #444444;
  color: #ff8519;
  box-sizing: border-box;
  outline: none;

  &:focus {
    background-color: #666666;
  }
`;
