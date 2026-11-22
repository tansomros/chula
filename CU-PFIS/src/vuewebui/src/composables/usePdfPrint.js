export function usePdfPrint() {
  function printCurrentView() {
    window.print()
  }

  return {
    printCurrentView,
  }
}
